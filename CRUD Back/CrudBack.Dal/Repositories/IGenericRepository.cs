namespace CrudBack.Dal.Repositories
{
    public interface IGenericRepository<TEntityModel> where TEntityModel : class
    {
        Task<IQueryable<TEntityModel>> Listar();

        Task<TEntityModel> Obtener(int id);

        Task<bool> Insertar(TEntityModel model);

        Task<bool> Actualizar(TEntityModel model);

        Task<bool> Eliminar(int id);
    }
}
