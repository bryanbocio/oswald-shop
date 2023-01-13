using API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Error
{

    [Route("errors/{code}")]
    [ApiExplorerSettings(IgnoreApi =true)]
    public class ErrorController : BaseApiController
    {
        public IActionResult Error(int statusCode)
        {
            return new ObjectResult(new ApiResponse(statusCode));
        }
    }
}
