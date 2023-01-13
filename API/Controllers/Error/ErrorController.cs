using API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Error
{

    [Route("errors/{code}")]
    public class ErrorController : BaseApiController
    {
        public IActionResult Error(int statusCode)
        {
            return new ObjectResult(new ApiResponse(statusCode));
        }
    }
}
