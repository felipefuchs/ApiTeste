using System.ComponentModel.DataAnnotations;
using Npgsql;
using System;
using System.Collections.Generic;

namespace SellApiData
{
    // Classe para cadastro de usuário no projeto SellApiData
    public class UsuarioCadastro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = null!;

        [Required]
        [StringLength(200, MinimumLength = 6)]
        public string Senha { get; set; } = null!;

        public bool Active { get; set; }

        private readonly PostgresConnection _connection;

        public UsuarioCadastro(PostgresConnection connection)
        {
            _connection = connection;
        }

        public void InsertUsuario(UsuarioCadastro usuario)
        {
            const string query = "INSERT INTO usuarios (nome, senha, active) VALUES (@nome, @senha, @active) RETURNING id;";

            using var cmd = _connection.CreateCommand();
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@nome", usuario.Nome);
            cmd.Parameters.AddWithValue("@senha", usuario.Senha);
            cmd.Parameters.AddWithValue("@active", usuario.Active);

            _connection.Open();
            usuario.Id = Convert.ToInt32(cmd.ExecuteScalar());
            _connection.Close();
        }

        public void UpdateUsuario(UsuarioCadastro usuario)
        {
            const string query = "UPDATE usuarios SET nome = @nome, senha = @senha, active = @active WHERE id = @id;";

            using var cmd = _connection.CreateCommand();
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@id", usuario.Id);
            cmd.Parameters.AddWithValue("@nome", usuario.Nome);
            cmd.Parameters.AddWithValue("@senha", usuario.Senha);
            cmd.Parameters.AddWithValue("@active", usuario.Active);

            _connection.Open();
            cmd.ExecuteNonQuery();
            _connection.Close();
        }

        public UsuarioCadastro? SelectUsuarioById(int id)
        {
            const string query = "SELECT id, nome, senha, active FROM usuarios WHERE id = @id;";

            using var cmd = _connection.CreateCommand();
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@id", id);

            _connection.Open();
            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                var usuario = new UsuarioCadastro(_connection)
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Senha = reader.GetString(2),
                    Active = reader.GetBoolean(3)
                };

                _connection.Close();
                return usuario;
            }

            _connection.Close();
            return null;
        }

        public List<UsuarioCadastro> SelectAllUsuarios()
        {
            const string query = "SELECT id, nome, senha, active FROM usuarios;";
            var usuarios = new List<UsuarioCadastro>();

            using var cmd = _connection.CreateCommand();
            cmd.CommandText = query;

            _connection.Open();
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                usuarios.Add(new UsuarioCadastro(_connection)
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Senha = reader.GetString(2),
                    Active = reader.GetBoolean(3)
                });
            }

            _connection.Close();
            return usuarios;
        }
    }
}
