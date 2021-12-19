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
    public class CreateCampaignCommandHandler : IRequestHandler<CreateCampaignCommand, Response<CreateCampaignCommandResult>>
    {
        private readonly ICampaignRepository _campaignRepository;
        private readonly IProductQueries _productQueries;

        public CreateCampaignCommandHandler(ICampaignRepository campaignRepository, IProductQueries productQueries)
        {
            _campaignRepository = campaignRepository;
            _productQueries = productQueries;
        }
        public async Task<Response<CreateCampaignCommandResult>> Handle(CreateCampaignCommand request, CancellationToken cancellationToken)
        {
                var product = _productQueries.GetProductInfoByCode(request.ProductCode);
                if (!product.Succeeded)
                    return Response<CreateCampaignCommandResult>.Fail(ResponseMessage.Error.NotFoundProduct);

                var campaign = new Campaign()
                {
                    Name = request.Name,
                    ProductId = product.Data.Id,
                    IsActive = true,
                    StartDate = DateTime.Now.Date,
                    EndDate = DateTime.Now.Date.AddHours(request.Duration),//duration
                    Limit = request.Limit,
                    DiscountPercentage = 5,// her saat başı increase time*discounpercentage düşecek
                    TargetSaleCount = request.TargetSaleCount,

                };
                _campaignRepository.Add(campaign);
                _campaignRepository.Save();

                return Response<CreateCampaignCommandResult>.Success(ResponseMessage.Success.CreateCampaign(campaign.Name,
                    product.Data.Code,request.Duration,campaign.Limit,campaign.TargetSaleCount));
            
        }
    }
}
