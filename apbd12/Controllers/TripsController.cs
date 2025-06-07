using apbd12.DTOs.Request;
using apbd12.Services;
using Microsoft.AspNetCore.Mvc;

namespace apbd12.Controllers;
[ApiController]
[Route("api/{controller}")]
public class TripsController : ControllerBase
{
    private readonly ITripsService _tripsService;
    private readonly IClientsService _clientsService;
    
    public TripsController(ITripsService tripsService, IClientsService clientsService)
    {
        _clientsService = clientsService;
        _tripsService = tripsService;
    }
    
    
    [HttpGet]
    public async Task<IActionResult> GetTripsAsync(CancellationToken token, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _tripsService.GetTripsAsync(token, page, pageSize);
      

        return new OkObjectResult(result);
    }
    
    [HttpPost("{idTrip}/clients")]
    public async Task<IActionResult> AddClientToTripAsync(CancellationToken token, int idTrip, [FromBody] ClientRequestDto dto)
    {
        var (success, error) = await _clientsService.AddClientToTripAsync(token, dto, idTrip);
        
        if (!success)
        {
            return new NotFoundObjectResult(error);
        }
        return new OkResult();
    }

}