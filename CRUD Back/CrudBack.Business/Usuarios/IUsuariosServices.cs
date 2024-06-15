using CrudBack.Models;

namespace CrudBack.Business.Usuarios
{
    public interface IUsuariosServices
    {
        Task<bool> Registrar(Usuario model);
        Task<IQueryable<Usuario>> Listar();
        Task<Usuario> Obtener(int id);
        Task<Usuario> Authenticate(string username, string password);
    }
}
