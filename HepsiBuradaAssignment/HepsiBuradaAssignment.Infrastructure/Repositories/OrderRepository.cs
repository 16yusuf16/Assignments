using HepsiBuradaAssignment.Domain.Entities;
using HepsiBuradaAssignment.Domain.Interfaces;
using HepsiBuradaAssignment.Infrastructure.Data.Context;

namespace HepsiBuradaAssignment.Infrastructure.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(AssignmentContext context) : base(context)
        {
        }
    }
}
