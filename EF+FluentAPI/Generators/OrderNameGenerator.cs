using EF_FluentAPI.DbContexts;

namespace EF_FluentAPI.Generators
{
    public class OrderNameGenerator : IGenerator
    {
        public string Generate(DBContext dbContext)
        {
            var orders = dbContext.Orders.Where(u => u.IsCompleted == true).ToList();
            
            var random = new Random();
            var name = "";
            if (orders is not null)
            {
                foreach (var order in orders)
                {
                    name = random.Next(0, 999999).ToString("D6");
                    if (name != order.Name) return name;
                }
            } else return random.Next(0, 999999).ToString("D6");

            throw new Exception("Все номера заняты!");
        }
    }
}
