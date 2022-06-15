using Microsoft.AspNetCore.Mvc.Filters;

namespace EF_FluentAPI.Logging
{
    public interface ILogger
    {
        void LogInfo(string message);
    }
}
