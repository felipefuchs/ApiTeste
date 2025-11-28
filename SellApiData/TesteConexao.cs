using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SellApiData
{
    public class Teste
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        // adicione outras propriedades conforme a tabela
    }

    public class TesteConexao
    {
        private readonly string _connectionString;

        public TesteConexao(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Retorna todos os registros da tabela "teste" mapeados para objetos Teste
        public List<Teste> GetAll()
        {
            var lista = new List<Teste>();

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = conn.CreateCommand())
            {   
                cmd.CommandText = "SELECT * FROM teste";
                conn.Open();

                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        var item = new Teste();

                        // Mapeia colunas comuns; envolvemos em try/catch caso a coluna não exista
                        try { item.Id = rdr["Id"] != DBNull.Value ? Convert.ToInt32(rdr["Id"]) : 0; } catch { }
                        try { item.Nome = rdr["Nome"] != DBNull.Value ? rdr["Nome"].ToString() : null; } catch { }

                        lista.Add(item);
                    }
                }
            }

            return lista;
        }
    }
}