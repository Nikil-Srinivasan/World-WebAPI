namespace World.API.DTO.State
{
    public class CreateStatesDto
    {
        public string Name { get; set; }
        public decimal population { get; set; }
        public int CountryId { get; set; }

    }
}
