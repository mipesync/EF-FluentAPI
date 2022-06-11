using EF_FluentAPI.DbContexts;

namespace EF_FluentAPI.Generators
{
    public class OrderNameGenerator
    {
        /*private char[] _char = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
            'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};*/

        public string Generate(DBContext dbContext)
        {
            var orders = dbContext.Orders.Where(u => u.IsCompleted == true);
            
            var random = new Random();
            var name = "";
            foreach (var order in orders)
            {
                name = random.Next(0, 999999).ToString();
                if (name != order.Name) return name;
            }

            throw new Exception("Все номера заняты!");
        }
    }
}
