using apbd12.DTOs.Request;
using apbd12.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace apbd12.Controllers;

[ApiController]
[Route("api/{controller}")]
public class ClientsController : ControllerBase
{
    private readonly IClientsService _clientsService;
    
    public ClientsController(IClientsService clientsService)
    {
        _clientsService = clientsService;
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClientByIdAsync(CancellationToken token, int id)
    {
        var (success, error) = await _clientsService.DeleteClientByIdAsync(token, id);
        
        if (!success)
        {
            return new NotFoundObjectResult(error);
        }
        return new OkResult();
    }
}