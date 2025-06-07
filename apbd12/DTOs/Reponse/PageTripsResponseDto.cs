namespace apbd12.DTOs.Reponse;

public class PageTripsResponseDto
{
    public int PageNum { get; set; }
    public int PageSize { get; set; }
    public int AllPages { get; set; }
    public List<TripResponseDto> Trips { get; set; } = new List<TripResponseDto>();
}