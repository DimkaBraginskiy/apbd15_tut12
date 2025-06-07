using apbd12.Data;
using apbd12.Models;
using Microsoft.EntityFrameworkCore;

namespace apbd12.Repositories;

public class ClientsRepository : IClientsRepository
{
    private readonly TripContext _context;
    
    public ClientsRepository(TripContext context)
    {
        _context = context;
    }

    public async Task<Client?> GetClientByIdAsync(CancellationToken token, int id)
    {
        return await _context.Clients.FindAsync(new object[] { id }, token);
    }

    public async Task<bool> HasAnyTripsAsync(CancellationToken token, int id)
    {
        return await _context.ClientTrips
            .AnyAsync(ct => ct.IdClient == id, token);
    }

    public async Task DeleteClientByIdAsync(CancellationToken token, Client client)
    {
        _context.Clients.Remove(client);
        await _context.SaveChangesAsync(token);
    }
}