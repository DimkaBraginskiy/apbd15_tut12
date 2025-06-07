using apbd12.DTOs.Reponse;

namespace apbd12.Services;

public interface ITripsService
{
    public Task<PageTripsResponseDto?> GetTripsAsync(CancellationToken token,  int page, int pageSize);
}