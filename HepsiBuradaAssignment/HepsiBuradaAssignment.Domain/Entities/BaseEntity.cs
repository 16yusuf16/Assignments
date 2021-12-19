using HepsiBuradaAssignment.Domain.Interfaces;

namespace HepsiBuradaAssignment.Domain.Entities
{
    public class BaseEntity : IEfEntity
    {
        public int Id { get; set; }
    }
}
