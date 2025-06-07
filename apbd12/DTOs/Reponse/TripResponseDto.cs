namespace apbd12.DTOs.Reponse;

public class TripResponseDto
{
    public string Name { get; set; }
    public String Description { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public int MaxPeople { get; set; }
    public List<CountryResponseDto> Countries { get; set; } = new List<CountryResponseDto>();
    public List<ClientResponseDto> Clients { get; set; } = new List<ClientResponseDto>();
    
    
}