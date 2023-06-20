using Microsoft.EntityFrameworkCore;
using World.API.Data;
using World.API.Repository.IRepository;

namespace World.API.Repository
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CountryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        

        public async Task Update(Country country)
        {
            _dbContext.Countries.Update(country);
            _dbContext.SaveChanges();
        }
    }
}
