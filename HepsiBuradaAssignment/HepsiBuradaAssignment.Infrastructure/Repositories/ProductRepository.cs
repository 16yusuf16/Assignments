using System.Linq;
using HepsiBuradaAssignment.Domain.Entities;
using HepsiBuradaAssignment.Domain.Interfaces;
using HepsiBuradaAssignment.Infrastructure.Data.Context;

namespace HepsiBuradaAssignment.Infrastructure.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        private AssignmentContext  _context;
        public ProductRepository(AssignmentContext context) : base(context)
        {
            _context = context;
        }

        public Product GetProductInfoByCode(string code)
        {
            return string.IsNullOrWhiteSpace(code) ? null : _context.Products.FirstOrDefault(x => x.Code == code);
        }
    }
}
