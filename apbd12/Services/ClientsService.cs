using apbd12.DTOs.Request;
using apbd12.Models;
using apbd12.Repositories;

namespace apbd12.Services;

public class ClientsService : IClientsService
{
    private readonly IClientsRepository _clientsRepository;
    private readonly ITripsRepository _tripsRepository;
    
    public ClientsService(IClientsRepository clientsRepository, ITripsRepository tripsRepository)
    {
        _clientsRepository = clientsRepository;
        _tripsRepository = tripsRepository;
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

    public async Task<(bool Success, string? Error)> AddClientToTripAsync(CancellationToken token, ClientRequestDto dto, int IdTrip)
    {
        // 1. Check if client already exists by PESEL
        if (await _tripsRepository.ClientExistsByPeselAsync(token, dto.Pesel))
        {
            return (false, "Client with this PESEL already exists.");
        }

        // 2. Check if trip exists and its date is in the future
        var trip = await _tripsRepository.GetTripByIdAsync(token, IdTrip);
        if (trip == null)
        {
            return (false, "Trip does not exist.");
        }

        if (trip.DateFrom <= DateTime.Now)
        {
            return (false, "Trip has already started or finished.");
        }

        // 3. Check if client already assigned to trip
        if (await _tripsRepository.IsClientAlreadyInTripAsync(token, dto.Pesel, IdTrip))
        {
            return (false, "Client is already registered for this trip.");
        }

        // 4. Create and insert Client
        var client = new Client
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Telephone = dto.Telephone,
            Pesel = dto.Pesel
        };

        await _tripsRepository.AddClieentAsync(token, client);

        // Get the client's ID from DB
        var insertedClient = await _clientsRepository.GetClientByPeselAsync(token, dto.Pesel);
        if (insertedClient == null)
        {
            return (false, "Failed to retrieve inserted client.");
        }

        // 5. Create and insert ClientTrip
        var clientTrip = new ClientTrip
        {
            IdClient = insertedClient.IdClient,
            IdTrip = trip.IdTrip,
            RegisteredAt = DateTime.Now,
            PaymentDate = dto.PaymentDate
        };

        await _tripsRepository.AddClientTripAsync(token, clientTrip);

        return (true, null);
    }
}