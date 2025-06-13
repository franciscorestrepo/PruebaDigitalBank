using System.ComponentModel.DataAnnotations;

namespace Usuario.Web.Models
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }

        [Required]
        public string? Nombre { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        public string? Sexo { get; set; }
    }
}
