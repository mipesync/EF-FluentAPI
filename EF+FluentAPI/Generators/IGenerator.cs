using EF_FluentAPI.DbContexts;

namespace EF_FluentAPI.Generators
{
    public interface IGenerator
    {
        public string Generate(DBContext dBContext);
    }
}
