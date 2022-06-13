using EF_FluentAPI__Front_.Models;
using System.Text.Json.Serialization;

namespace EF_FluentAPI.Models.Intermediate_Entities;

public class ProductCart
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int CartId { get; set; }

    [JsonIgnore]
    public Product? Product { get; set; }
    [JsonIgnore]
    public Cart? Cart { get; set; }
}
