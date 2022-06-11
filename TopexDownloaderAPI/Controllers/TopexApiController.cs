using Microsoft.AspNetCore.Mvc;
using System.Text;
using TopexDownloaderAPI.Services;
using tp = TopexDownloaderAPI.Services.TopexServices;
using gis = TopexDownloaderAPI.Services.GeographicServices;

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

        [HttpGet(Name = "Get Gravity")]
        public async Task<IActionResult> gravity(double north, double west, double east, double south, bool download)
        {
            List<TopexDataModel> results = new();
            try
            {   
                if (north <= south) throw new ApplicationException("north coordinate is smaller or equal than south!");
                else if (east <= west) throw new ApplicationException("east coordinate is smaller or equal than west!");

                if (gis.LatLonRectangle(north, west, east, south) > 5e10) throw new ApplicationException("the area is too big!");

                var topexRequest = tp.GetFromTopex(north, west, east, south, 0.1);
                await topexRequest;

                results = topexRequest.Result;

                if (download)
                {
                    var file = await results.WriteCSV(true);
                    return File(file, "text/csv", $"{DateTime.Now}-gravity.csv");
                }

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

        [HttpGet(Name = "Get Elevation")]
        public async Task<IActionResult> elevation(double north, double west, double east, double south, bool download)
        {
            List<TopexDataModel> results = new();

            try
            {
                if (north < south) throw new ApplicationException("north coordinate is smaller than south!");
                else if (east < west) throw new ApplicationException("east coordinate is smaller than west!");

                if (gis.LatLonRectangle(north, west, east, south) > 5e10) throw new ApplicationException("the area is too big!");

                var topexRequest = tp.GetFromTopex(north, west, east, south, 1);
                await topexRequest;

                results = topexRequest.Result;

                if (download)
                {
                    var file = await results.WriteCSV(true);
                    return File(file, "text/csv", $"{DateTime.Now}-elevation.csv");
                }

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