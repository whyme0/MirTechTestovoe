namespace MirTechTestovoe.Server.Data.Repository
{
    public interface IRepository<T, TCreateDto, TUpdateDto>
    {
        T? GetById(int id);
        T Add(TCreateDto entity);
        T? Update(int id, TUpdateDto entity);
        void Delete(T entity);
        void SaveChanges();
    }
}
