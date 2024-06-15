using CrudBack.Dal.Repositories;
using CrudBack.Models;

namespace CrudBack.Business.TareasPendientes
{
    public class TareasPendientesService : ITareasPendientesService
    {
        private readonly IGenericRepository<TareasPendiente> _tareasPendiente;

        public TareasPendientesService(IGenericRepository<TareasPendiente> tareasPendiente)
        {
            _tareasPendiente = tareasPendiente;
        }

        public async Task<IQueryable<TareasPendiente>> Listar()
        {
            return await _tareasPendiente.Listar();
        }

        public async Task<TareasPendiente> Obtener(int id)
        {
            return await _tareasPendiente.Obtener(id);
        }

        public async Task<bool> Insertar(TareasPendiente model)
        {
            return await _tareasPendiente.Insertar(model);
        }

        public async Task<bool> Actualizar(TareasPendiente model)
        {
            return await _tareasPendiente.Actualizar(model);
        }

        public async Task<bool> Eliminar(int id)
        {
            return await _tareasPendiente.Eliminar(id);
        }
    }
}
