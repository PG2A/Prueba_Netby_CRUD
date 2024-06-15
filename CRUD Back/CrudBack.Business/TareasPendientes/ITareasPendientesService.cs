using CrudBack.Models;

namespace CrudBack.Business.TareasPendientes
{
    public interface ITareasPendientesService
    {
        Task<IQueryable<TareasPendiente>> Listar();

        Task<TareasPendiente> Obtener(int id);

        Task<bool> Insertar(TareasPendiente model);

        Task<bool> Actualizar(TareasPendiente model);

        Task<bool> Eliminar(int id);
    }
}
