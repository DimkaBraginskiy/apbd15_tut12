using apbd12.Models;

namespace apbd12.Repositories;

public interface ITripsRepository
{
    public Task<(List<Trip> Trips, int TotalCount)> GetTripsAsync(CancellationToken token,int page, int pageSize);
    
    public Task<bool> ClientExistsByPeselAsync(CancellationToken token, string pesel);
    public Task<bool> IsClientAlreadyInTripAsync(CancellationToken token, string pesel, int idTrip);
    public Task<Trip?> GetTripByIdAsync(CancellationToken token, int idTrip);
    public Task AddClieentAsync(CancellationToken token, Client client);
    public Task AddClientTripAsync(CancellationToken token, ClientTrip clientTrip);
}