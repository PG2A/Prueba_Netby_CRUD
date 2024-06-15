using CrudBack.Dal.DataContext;
using CrudBack.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudBack.Dal.Repositories
{
    public class TareasPendienteRepository : IGenericRepository<TareasPendiente>
    {
        private PedroGutierrezContext _dbContext;

        public TareasPendienteRepository(PedroGutierrezContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<IQueryable<TareasPendiente>> Listar()
        {
            IQueryable<TareasPendiente> queryAll = _dbContext.TareasPendientes;
            return queryAll;
        }

        public async Task<TareasPendiente> Obtener(int id)
        {
            return await _dbContext.TareasPendientes.FindAsync(id) ?? new TareasPendiente();
        }

        public async Task<bool> Insertar(TareasPendiente model)
        {
            _dbContext.TareasPendientes.Add(model);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Actualizar(TareasPendiente model)
        {
            _dbContext.TareasPendientes.Update(model);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Eliminar(int id)
        {
            TareasPendiente tareasPendiente = await _dbContext.TareasPendientes.FindAsync(id) ?? new TareasPendiente();
            _dbContext.TareasPendientes.Remove(tareasPendiente);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
