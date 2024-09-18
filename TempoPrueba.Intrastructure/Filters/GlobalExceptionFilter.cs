using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;
using TempoPrueba.Core.Exceptions;

namespace InsttanttFlujos.Infrastructure.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public readonly IWebHostEnvironment _hostingEnviroment;
        private readonly IModelMetadataProvider _modelMetadataProvier;

        public GlobalExceptionFilter(IWebHostEnvironment hostingEnviroment, IModelMetadataProvider modelMetadataProvier)
        {
            _hostingEnviroment = hostingEnviroment;
            _modelMetadataProvier = modelMetadataProvier;
        }

        public void OnException(ExceptionContext context)
        {
            //--------------------------------------------------------//
            //--   Para tlos errores tipo Rxception de status 400   --//
            //--------------------------------------------------------//
            if (context.Exception.GetType() == typeof(BusinessException))
            {
                var exception = (BusinessException)context.Exception;
                var validation = new
                {
                    Status = 400,
                    Title = "Bad Request",
                    Detail = exception.Message
                };

                var json = new
                {
                    errors = new[] { validation }
                };

                context.Result = new BadRequestObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.ExceptionHandled = true;
            }
            else
            {
                //------------------------------------------------//
                //--   Para todos los demas Errores generados   --//
                //------------------------------------------------//
                //var exception = context.Exception;

                //var validation = new
                //{
                //    Program = _hostingEnviroment.ApplicationName,
                //    Tipo = context.Exception.GetType().ToString(),
                //    Title = exception.Message,
                //    Detail = exception.InnerException.Message.ToString(),
                //};

                //var json = new
                //{
                //    Errors = new[] { validation }
                //};

                //context.Result = new JsonResult(json);
            }

        }
    }
}
