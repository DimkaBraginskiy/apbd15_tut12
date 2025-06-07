using apbd12.DTOs.Request;

namespace apbd12.Services;

public interface IClientsService
{
    public Task<(bool Success, string? Error)> DeleteClientByIdAsync(CancellationToken token, int id);

    public Task<(bool Success, string? Error)> AddClientToTripAsync(CancellationToken token, ClientRequestDto dto, int IdTrip);
}