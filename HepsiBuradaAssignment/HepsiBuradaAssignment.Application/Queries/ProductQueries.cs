using AutoMapper;
using HepsiBuradaAssignment.Application.Models;
using HepsiBuradaAssignment.Application.Response;
using HepsiBuradaAssignment.Domain.Interfaces;

namespace HepsiBuradaAssignment.Application.Queries
{

    public interface IProductQueries
    {
        Response<ProductDtoModel> GetProductInfoByCode(string code);
    }

    public class ProductQueries : IProductQueries
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductQueries(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public Response<ProductDtoModel> GetProductInfoByCode(string code)
        {
            var product = _productRepository.GetProductInfoByCode(code);

            if (product is null)
                return Response<ProductDtoModel>.Fail(ResponseMessage.Error.NotFoundProduct);

            var result = _mapper.Map<ProductDtoModel>(product);

            return Response<ProductDtoModel>.Success(result, ResponseMessage.Success.GetProductByCode(product.Code, product.Price, product.Stock));
        }
    }
}
