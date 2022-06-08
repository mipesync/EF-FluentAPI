using System.Text.Json.Serialization;

namespace EF_FluentAPI__Front_.Models
{
    public class Credential
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Passhash { get; set; } = null!;
        public string? CustomerId { get; set; }

        public Customer? Customer { get; set; }

    }
}
