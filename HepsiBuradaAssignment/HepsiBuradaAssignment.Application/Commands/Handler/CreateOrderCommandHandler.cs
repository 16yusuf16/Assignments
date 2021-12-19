using System;
using System.Threading;
using System.Threading.Tasks;
using HepsiBuradaAssignment.Application.Commands.CommandResult;
using HepsiBuradaAssignment.Application.Queries;
using HepsiBuradaAssignment.Application.Response;
using HepsiBuradaAssignment.Domain.Entities;
using HepsiBuradaAssignment.Domain.Interfaces;
using MediatR;
namespace HepsiBuradaAssignment.Application.Commands.Handler
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response<CreateOrderCommandResult>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICampaignRepository _campaignRepository;

        public CreateOrderCommandHandler( IOrderRepository orderRepository,
            IProductRepository productRepository, ICampaignRepository campaignRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _campaignRepository = campaignRepository;

        }
        public async Task<Response<CreateOrderCommandResult>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
                var order = new Order();
                var product = _productRepository.GetProductInfoByCode(request.ProductCode);
                if(product is null)
                {
                    return Response<CreateOrderCommandResult>.Fail(ResponseMessage.Error.NotFoundProduct);
                }
                if(product.Stock == 0)
                {
                    return Response<CreateOrderCommandResult>.Fail(ResponseMessage.Error.InsufficientProduct);
                }

                if(product.Stock < request.Quantity)
                {
                    return Response<CreateOrderCommandResult>.Fail(ResponseMessage.Error.ProductStockInsufficientRequestedQuantity);
                }

           
                var campaign = _campaignRepository.GetCampaignByProductCode(request.ProductCode,DateTime.Now.Date.AddHours(request.DifferenceTime));
                if(campaign is null)
                {
                    order.Price = product.Price;
                    order.TotalPrice = product.Price * request.Quantity;
                }
                else
                {
                    //price manipulation limit (ürün fiyatı üzerinden fazla bu orana kadar indirilebilir)
                    var disccountPercent = (campaign.DiscountPercentage * request.DifferenceTime);
                    var discountPrice = product.Price - (product.Price * (campaign.Limit> disccountPercent ? disccountPercent :campaign.Limit) / 100);
                   
                    order.Price = discountPrice;
                    order.TotalPrice = discountPrice * request.Quantity;
                    order.Discount = campaign.DiscountPercentage;
                    order.CampaignId = campaign.Id;
                }
                order.CreatedDate = DateTime.Now;
                order.Quantity = request.Quantity;
                order.ProductId = product.Id;

                _orderRepository.Add(order);
               

                product.Stock = product.Stock - request.Quantity;
                _productRepository.Update(product);

                _orderRepository.Save();
                _productRepository.Save();

                if(campaign is not null)
                {
                    campaign.TotalSaleCount += request.Quantity;
                    _campaignRepository.Update(campaign);
                    _campaignRepository.Save();
                }

                return Response<CreateOrderCommandResult>.Success(ResponseMessage.Success.CreateOrder(request.ProductCode, request.Quantity));

            
        }
    }
}