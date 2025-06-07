using apbd12.Services;
using Microsoft.AspNetCore.Mvc;

namespace apbd12.Controllers;
[Controller]
[Route("api/{controller}")]
public class TripsController : ControllerBase
{
    private readonly ITripsService _tripsService;
    
    public TripsController(ITripsService tripsService)
    {
        _tripsService = tripsService;
    }
    
    
    [HttpGet]
    public async Task<IActionResult> GetTripsAsync(CancellationToken token, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _tripsService.GetTripsAsync(token, page, pageSize);
      

        return new OkObjectResult(result);
    }

}