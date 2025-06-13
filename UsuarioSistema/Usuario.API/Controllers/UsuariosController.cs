using Microsoft.AspNetCore.Mvc;
using Usuario.API.Services;
using Usuario.Data.Entities;


namespace Usuario.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuariosController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            _usuarioService = new UsuarioService(connectionString);
        }

        [HttpGet]
        public IActionResult GetUsuarios()
        {
            var lista = _usuarioService.ObtenerUsuarios();
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public IActionResult GetUsuarioById(int id)
        {
            var usuario = _usuarioService.ObtenerUsuarioPorId(id);
            if (usuario == null)
                return NotFound(new { mensaje = $"No se encontró el usuario con ID {id}" });

            return Ok(usuario);
        }

        [HttpPost]
        public IActionResult CreateUsuario([FromBody] User usuario) 
        {
            var lista = _usuarioService.CrearUsuario(usuario);
            return Ok(lista);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUsuario(int id, [FromBody] User usuario) 
        {
            var lista = _usuarioService.ActualizarUsuario(id, usuario);
            return Ok(lista);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUsuario(int id)
        {
            var lista = _usuarioService.EliminarUsuario(id);
            return Ok(lista);
        }
    }
}

