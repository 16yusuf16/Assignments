using System;
namespace HepsiBuradaAssignment.Domain.Entities
{
    public class Campaign : BaseEntity
    {
        public string Name { get; set; }
        public int ProductId { get; set; }
        public bool IsActive { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPercentage { get; set; }
        public int Limit { get; set; }
        public int TargetSaleCount { get; set; }
        public int TotalSaleCount { get; set; }

    }
}
