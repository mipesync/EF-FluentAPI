namespace EF_FluentAPI__Front_.Models.Intermediate_Entities
{
    public class ProductOrder
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }

        public Product? Product { get; set; }
        public Order? Order { get; set; }
    }
}