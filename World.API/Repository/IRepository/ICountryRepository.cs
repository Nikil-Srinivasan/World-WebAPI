namespace World.API.Repository.IRepository
{
    public interface ICountryRepository : IGenericRepository<Country>
    {
        Task Update (Country country);
    }
}
