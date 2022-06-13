using System.Text.Json.Serialization;

namespace EF_FluentAPI.Models.Intermediate_Entities
{
    public class ProductOrder
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }

        [JsonIgnore]
        public Product? Product { get; set; }
        [JsonIgnore]
        public Order? Order { get; set; }
    }
}
