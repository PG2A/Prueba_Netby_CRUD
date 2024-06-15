using CrudBack.Dal.DataContext;
using CrudBack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudBack.Dal.Repositories
{
    public class UsuarioRepository : IGenericRepository<Usuario>
    {
        private PedroGutierrezContext _dbContext;

        public UsuarioRepository(PedroGutierrezContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Actualizar(Usuario model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Insertar(Usuario model)
        {
            _dbContext.Usuario.Add(model);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IQueryable<Usuario>> Listar()
        {
            IQueryable<Usuario> queryAll = _dbContext.Usuario;
            return queryAll;
        }

        public async Task<Usuario> Obtener(int id)
        {
            return await _dbContext.Usuario.FindAsync(id) ?? new Usuario();
        }
    }
}
