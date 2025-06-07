using apbd12.Repositories;

namespace apbd12.Services;

public class ClientsService : IClientsService
{
    private readonly IClientsRepository _clientsRepository;
    
    public ClientsService(IClientsRepository clientsRepository)
    {
        _clientsRepository = clientsRepository;
    }

    public async Task<(bool Success, string? Error)> DeleteClientByIdAsync(CancellationToken token, int id)
    {
        var client = await _clientsRepository.GetClientByIdAsync(token, id);
        if (client == null)
        {
            return (false, $"Client with {id} was not found.");
        }
        
        if(await _clientsRepository.HasAnyTripsAsync(token, id))
        {
            return (false, "Cannot delete client with assigned trips");
        }
        
        await _clientsRepository.DeleteClientByIdAsync(token, client);
        
        return (true, null);
    }
}