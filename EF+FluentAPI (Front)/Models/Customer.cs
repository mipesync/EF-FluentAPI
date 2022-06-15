using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EF_FluentAPI__Front_.Models
{
    public class Customer
    {
        public Customer()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string Name { get; set; } = null!;
        [RegularExpression(@"^(\s*)?(\+)?([- _():=+]?\d[- _():=+]?){10,14}(\s*)?$",
         ErrorMessage = "Неверный формат номера телефона!")]
        public string Phone { get; set; } = null!;

        [JsonIgnore]
        public Credential? Credential { get; set; }
        [JsonIgnore]
        public Cart? Cart { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}
