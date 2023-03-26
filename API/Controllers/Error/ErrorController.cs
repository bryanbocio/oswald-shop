using API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Error
{

    [Route("errors/{code}")]
    [ApiExplorerSettings(IgnoreApi =true)]
    public class ErrorController : BaseApiController
    {
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiException(code));
        }
    }
}
