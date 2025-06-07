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
}