using Usuario.Data.Repositories;


namespace Usuario.API.Services
{
    public class UsuarioService
    {
        private readonly UsuarioRepository _repositorio;

        public UsuarioService(string connectionString)
        {
            _repositorio = new UsuarioRepository(connectionString);
        }

        public Usuario.Data.Entities.User? ObtenerUsuarioPorId(int id)
        {
            return _repositorio.ObtenerPorId(id);
        }

        public List<Usuario.Data.Entities.User> ObtenerUsuarios()
        {
            return _repositorio.EjecutarOperacion("SELECT");
        }

        public List<Usuario.Data.Entities.User> CrearUsuario(Usuario.Data.Entities.User usuario)
        {
            return _repositorio.EjecutarOperacion("INSERT", usuario);
        }

        public List<Usuario.Data.Entities.User> ActualizarUsuario(int id, Usuario.Data.Entities.User usuario)
        {
            usuario.Id = id;
            return _repositorio.EjecutarOperacion("UPDATE", usuario);
        }

        public List<Usuario.Data.Entities.User> EliminarUsuario(int id)
        {
            var usuario = new Usuario.Data.Entities.User { Id = id };
            return _repositorio.EjecutarOperacion("DELETE", usuario);
        }
    }
}
