using apbd12.Data;
using apbd12.Models;
using Microsoft.EntityFrameworkCore;

namespace apbd12.Repositories;

public class TripsRepository : ITripsRepository
{
    private readonly TripContext _context;

    public TripsRepository(TripContext context)
    {
        _context = context;
    }

    public async Task<(List<Trip> Trips, int TotalCount)> GetTripsAsync(CancellationToken token, int page, int pageSize)
    {
        var query = _context.Trips
            .Include(t => t.ClientTrips).ThenInclude(ct => ct.IdClientNavigation)
            .Include(t => t.IdCountries)
            .OrderByDescending(t => t.DateFrom);
        
        var totalCount = await query.CountAsync(token);
        
        var trips = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(token);
        
        return (trips, totalCount);
    }
    
    public async Task<bool> ClientExistsByPeselAsync(CancellationToken token, string pesel)
    {
        return await _context.Clients.AnyAsync(c => c.Pesel == pesel, token);
    }
    
    public async Task<bool> IsClientAlreadyInTripAsync(CancellationToken token, string pesel, int idTrip)
    {
        return await _context.ClientTrips
            .Include(ct => ct.IdClientNavigation)
            .AnyAsync(ct => ct.IdTrip == idTrip && ct.IdClientNavigation.Pesel == pesel, token);
    }

    
    public async Task<Trip?> GetTripByIdAsync(CancellationToken token, int idTrip)
    {
        return await _context.Trips.FirstOrDefaultAsync(t => t.IdTrip == idTrip, token);
    }

    public async Task AddClieentAsync(CancellationToken token, Client client)
    {
        _context.Clients.Add(client);
        await _context.SaveChangesAsync(token);
    }

    public async Task AddClientTripAsync(CancellationToken token, ClientTrip clientTrip)
    {
        _context.ClientTrips.Add(clientTrip);
        await _context.SaveChangesAsync(token);
    }
}