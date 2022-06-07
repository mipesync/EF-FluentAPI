using System.Text.RegularExpressions;

namespace EF_FluentAPI__Front_.JsonDeserializer
{
    public class Deserializer
    {
        public string Deserialize(string data)
        {
            return data.Substring(data.IndexOf(':') + 1).Replace("\"", "").Replace("}", "");
        }
    }
}
