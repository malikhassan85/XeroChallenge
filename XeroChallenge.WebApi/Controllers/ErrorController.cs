using Microsoft.AspNetCore.Mvc;

namespace XeroChallenge.WebApi.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
            [Route("/error")]
            public IActionResult Error() => Problem();
    }
}
