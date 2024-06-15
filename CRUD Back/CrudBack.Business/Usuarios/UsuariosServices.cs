using CrudBack.Dal.Repositories;
using CrudBack.Models;

namespace CrudBack.Business.Usuarios
{
    public class UsuariosServices : IUsuariosServices
    {
        private readonly IGenericRepository<Usuario> _usuarios;

        public UsuariosServices(IGenericRepository<Usuario> usuarios)
        {
            _usuarios = usuarios;
        }

        public async Task<bool> Registrar(Usuario model)
        {
            var listaUsuarios = await _usuarios.Listar();

            var existeUsuario = listaUsuarios.FirstOrDefault(x => x.Username == model.Username);
            if (existeUsuario is not null)
            {
                throw new Exception("El Usuario ingresado ya existe");
            }

            var existeEmail = listaUsuarios.FirstOrDefault(x => x.Email == model.Email);
            if (existeEmail is not null)
            {
                throw new Exception("El Email Ingresado ya existe");
            }
            return await _usuarios.Insertar(model);
        }

        public async Task<IQueryable<Usuario>> Listar()
        {
            return await _usuarios.Listar();
        }

        public async Task<Usuario> Obtener(int id)
        {
            return await _usuarios.Obtener(id);
        }

        public async Task<Usuario> Authenticate(string username, string password)
        {
            var listaUsuarios = await _usuarios.Listar();
            return listaUsuarios.FirstOrDefault(x => x.Username == username && x.Password == password);
        }
    }
}
