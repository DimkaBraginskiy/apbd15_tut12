using apbd12.DTOs.Reponse;
using apbd12.Repositories;

namespace apbd12.Services;

public class TripsService : ITripsService
{
    private readonly ITripsRepository _tripsRepository;

    public TripsService(ITripsRepository tripsRepository)
    {
        _tripsRepository = tripsRepository;
    }


    public async Task<PageTripsResponseDto?> GetTripsAsync(CancellationToken token,  int page, int pageSize)
    {
        var (trips, totalCount) = await _tripsRepository.GetTripsAsync(token, page, pageSize);

        int allPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        var response = new PageTripsResponseDto()
        {
            PageNum = page,
            PageSize = pageSize,
            AllPages = allPages,
            Trips = trips.Select(t => new TripResponseDto
            {
                Name = t.Name,
                Description = t.Description,
                DateFrom = t.DateFrom,
                DateTo = t.DateTo,
                MaxPeople = t.MaxPeople,
                Countries = t.IdCountries.Select(c => new CountryResponseDto
                {
                    Name = c.Name
                }).ToList(),
                Clients = t.ClientTrips.Select(ct => new ClientResponseDto
                {
                    FirstName = ct.IdClientNavigation.FirstName,
                    LastName = ct.IdClientNavigation.LastName
                }).ToList()
            }).ToList()
        };
        
        return response;
    }
}