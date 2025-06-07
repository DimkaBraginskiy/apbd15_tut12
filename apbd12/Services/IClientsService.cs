namespace apbd12.Services;

public interface IClientsService
{
    public Task<(bool Success, string? Error)> DeleteClientByIdAsync(CancellationToken token, int id);
}