using Microsoft.AspNetCore.Mvc;
using System.Net;
using tp = TopexDownloaderAPI.TopexServices;

namespace TopexDownloaderAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[action]")]
    public class TopexApiController : ControllerBase
    {
        private readonly ILogger<TopexApiController> _logger;

        public TopexApiController(ILogger<TopexApiController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetGravity")]
        public async Task<IActionResult> gravity(double north, double west, double east, double south)
        {
            List<TopexDataModel> results = new();
            try
            {
                if (north < south) throw new ApplicationException("north coordinate is smaller than south!");
                else if (east < west) throw new ApplicationException("east coordinate is smaller than west!");

                var topexRequest = tp.GetFromTopex(north, west, east, south, 0.1);
                await topexRequest;

                results = topexRequest.Result;

            }
            catch (ApplicationException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { status = StatusCodes.Status400BadRequest, error = $"Application Error : {e.Message}" });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(results);
        }

        [HttpGet(Name = "GetElevation")]
        public async Task<IActionResult> elevation(double north, double west, double east, double south)
        {
            List<TopexDataModel> results = new();
            try
            {
                if (north < south) throw new ApplicationException("north coordinate is smaller than south!");
                else if (east < west) throw new ApplicationException("east coordinate is smaller than west!");

                var topexRequest = tp.GetFromTopex(north, west, east, south, 1);
                await topexRequest;

                results = topexRequest.Result;

            }
            catch (ApplicationException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { status = StatusCodes.Status400BadRequest, error = $"Application Error : {e.Message}" });
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(results);
        }
    }
}