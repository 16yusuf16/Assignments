namespace HepsiBuradaAssignment.Application.Models
{
    public class ProductDtoModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public bool IsDiscount { get; set; }
    }
}
