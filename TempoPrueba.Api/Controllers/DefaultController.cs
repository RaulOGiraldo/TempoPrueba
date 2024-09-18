using Microsoft.AspNetCore.Mvc;

namespace TempoPrueba.Api.Controllers
{
    [Route("/")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        [HttpGet]
        public string Index()
        {
            return "Running ...";
        }
    }
}
