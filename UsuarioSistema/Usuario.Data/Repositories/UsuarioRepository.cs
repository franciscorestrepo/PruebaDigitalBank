using Npgsql;


namespace Usuario.Data.Repositories
{
    public class UsuarioRepository
    {
        private readonly string _connectionString;

        public UsuarioRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Data.Entities.User> EjecutarOperacion(string operacion, Data.Entities.User usuario = null)
        {
            var lista = new List<Data.Entities.User>();

            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();

            using var cmd = new NpgsqlCommand("SELECT * FROM sp_crud_usuarios(@op, @id, @nombre, @fecha, @sexo)", conn);

            cmd.Parameters.AddWithValue("op", operacion);
            cmd.Parameters.AddWithValue("id", (object?)usuario?.Id ?? DBNull.Value);
            cmd.Parameters.AddWithValue("nombre", (object?)usuario?.Nombre ?? DBNull.Value);
            cmd.Parameters.AddWithValue("fecha", (object?)(usuario?.FechaNacimiento.Date) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("sexo", (object?)usuario?.Sexo ?? DBNull.Value);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Data.Entities.User
                {
                    Id = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    FechaNacimiento = reader.GetDateTime(2),
                    Sexo = reader.GetString(3)
                });
            }

            return lista;
        }

        public Data.Entities.User? ObtenerPorId(int id)
        {
            var resultado = EjecutarOperacion("SELECT_BY_ID", new Data.Entities.User { Id = id });
            return resultado.FirstOrDefault();
        }
    }
}
