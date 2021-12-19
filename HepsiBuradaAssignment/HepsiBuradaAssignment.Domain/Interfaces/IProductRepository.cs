using HepsiBuradaAssignment.Domain.Entities;

namespace HepsiBuradaAssignment.Domain.Interfaces
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
         Product GetProductInfoByCode(string code);
    }
}
