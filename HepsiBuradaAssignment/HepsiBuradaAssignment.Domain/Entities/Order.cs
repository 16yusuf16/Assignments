using System;
namespace HepsiBuradaAssignment.Domain.Entities
{
    public class Order : BaseEntity
    {
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsActive { get; set; }
        public int ProductId { get; set; }
        public int? CampaignId { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }

    }
}
