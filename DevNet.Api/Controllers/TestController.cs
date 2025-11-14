using Microsoft.AspNetCore.Mvc;

namespace DevNet.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        #region Public Methods

        [HttpGet("test")]
        public IActionResult Get()
        {
            return Ok("API is working!");
        }

        #endregion Public Methods
    }
}