using System.Threading;
using System.Threading.Tasks;
using HepsiBuradaAssignment.Application.Commands.CommandResult;
using HepsiBuradaAssignment.Application.Response;
using HepsiBuradaAssignment.Domain.Entities;
using HepsiBuradaAssignment.Domain.Interfaces;
using MediatR;

namespace HepsiBuradaAssignment.Application.Commands.Handler
{
    public class CreateProductCommandHandler:IRequestHandler<CreateProductCommand,Response<CreateProductCommandResult>>
    {
      
        private readonly IProductRepository _productRepository;

        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Response<CreateProductCommandResult>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
                if (string.IsNullOrWhiteSpace(request.Code))
                {
                    return Response<CreateProductCommandResult>.Fail(ResponseMessage.Error.ProductCouldntCreated);
                }
                var product = new Product()
                {
                    Code = request.Code,
                    Name = request.Code,
                    Price = request.Price,
                    Stock = request.Stock,
                };
                _productRepository.Add(product);
                _productRepository.Save();
                return Response<CreateProductCommandResult>.Success(ResponseMessage.Success.CreateProduct(product.Code,product.Price,product.Stock));
           
        }
    }
}
