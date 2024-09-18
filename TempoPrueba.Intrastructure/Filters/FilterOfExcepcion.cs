using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace InsttanttFlujos.Infrastructure.Filters
{
    public class FilterOfExcepcion : ExceptionFilterAttribute
    {
        private readonly ILogger<FilterOfExcepcion> _logger;

        public FilterOfExcepcion(ILogger<FilterOfExcepcion> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, context.Exception.Message);
            base.OnException(context);
        }
    }
}
