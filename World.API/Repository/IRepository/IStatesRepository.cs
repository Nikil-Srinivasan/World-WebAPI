namespace World.API.Repository.IRepository
{
    public interface IStatesRepository : IGenericRepository<States>
    {
        Task Update(States entity);
    }
}
