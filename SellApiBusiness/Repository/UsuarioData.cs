using System;
using System.Collections.Generic;
using Npgsql;
using SellApiBusiness.Model;
using SellApiBusiness.InfraExtruture.Context;

namespace SellApiBusiness.Repository
{
    // Classe para operações de usuário usando o modelo SellApiBusiness.Model.Usuario
    public class UsuarioData
    {
        private readonly PostgresConnection _connection;

        public UsuarioData(PostgresConnection connection)
        {
            _connection = connection;
        }

        public void InsertUsuario(Usuario usuario)
        {
            const string query = "INSERT INTO usuarios (nome, email, senha, active) VALUES (@nome, @email, @senha, @active) RETURNING id;";

            using var cmd = _connection.CreateCommand();
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@nome", usuario.Nome);
            cmd.Parameters.AddWithValue("@email", usuario.Email ?? string.Empty);
            cmd.Parameters.AddWithValue("@senha", usuario.Senha);
            cmd.Parameters.AddWithValue("@active", usuario.Ativo);

            _connection.Open();
            usuario.Id = Convert.ToInt32(cmd.ExecuteScalar());
            _connection.Close();
        }

        public void UpdateUsuario(Usuario usuario)
        {
            const string query = "UPDATE usuarios SET nome = @nome, email = @email, senha = @senha, active = @active WHERE id = @id;";

            using var cmd = _connection.CreateCommand();
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@id", usuario.Id);
            cmd.Parameters.AddWithValue("@nome", usuario.Nome);
            cmd.Parameters.AddWithValue("@email", usuario.Email ?? string.Empty);
            cmd.Parameters.AddWithValue("@senha", usuario.Senha);
            cmd.Parameters.AddWithValue("@active", usuario.Ativo);

            _connection.Open();
            cmd.ExecuteNonQuery();
            _connection.Close();
        }

        public Usuario? SelectUsuarioById(int id)
        {
            const string query = "SELECT id, nome, email, senha, active FROM usuarios WHERE id = @id;";

            using var cmd = _connection.CreateCommand();
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@id", id);

            _connection.Open();
            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                var usuario = new Usuario
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Email = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                    Senha = reader.GetString(3),
                    Ativo = reader.GetBoolean(4)
                };

                _connection.Close();
                return usuario;
            }

            _connection.Close();
            return null;
        }

        public List<Usuario> SelectAllUsuarios()
        {
            const string query = "SELECT id, nome, email, senha, active FROM usuarios;";
            var usuarios = new List<Usuario>();

            using var cmd = _connection.CreateCommand();
            cmd.CommandText = query;

            _connection.Open();
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                usuarios.Add(new Usuario
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Email = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                    Senha = reader.GetString(3),
                    Ativo = reader.GetBoolean(4)
                });
            }

            _connection.Close();
            return usuarios;
        }
    }
}
