namespace World.API.DTO.State
{
    public class UpdateStatesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal population { get; set; }
        public int CountryId { get; set; }
    }
}
