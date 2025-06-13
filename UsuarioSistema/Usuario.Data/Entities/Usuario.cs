

namespace Usuario.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string? Sexo { get; set; }
    }
}
