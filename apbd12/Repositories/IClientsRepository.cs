using apbd12.Models;

namespace apbd12.Repositories;

public interface IClientsRepository
{
    public Task<Client?> GetClientByIdAsync(CancellationToken token, int id);
    public Task<Client?> GetClientByPeselAsync(CancellationToken token, string pesel);
    public Task<bool> HasAnyTripsAsync(CancellationToken token, int id);
    public Task DeleteClientByIdAsync(CancellationToken token, Client client);
}