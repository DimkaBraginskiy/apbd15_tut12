using apbd12.Models;

namespace apbd12.Repositories;

public interface ITripsRepository
{
    public Task<(List<Trip> Trips, int TotalCount)> GetTripsAsync(CancellationToken token,int page, int pageSize);
}