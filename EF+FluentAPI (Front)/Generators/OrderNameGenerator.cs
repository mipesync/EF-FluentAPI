namespace EF_FluentAPI__Front_.Generators
{
    public class OrderNameGenerator
    {
        /*private char[] _char = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
            'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};*/

        public string Generate()
        {
            var random = new Random();
            return random.Next(0, 999999).ToString();
        }
    }
}
