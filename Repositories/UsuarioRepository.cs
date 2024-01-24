using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly string _connectionString;

    public UsuarioRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<IEnumerable<Usuario>> GetAll()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            return await connection.QueryAsync<Usuario>("SELECT * FROM Pessoa");
        }
    }

    public async Task<Usuario> GetById(int id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            return await connection.QueryFirstOrDefaultAsync<Usuario>("SELECT * FROM Pessoa WHERE Id = @Id", new { Id = id });
        }
    }

    public async Task<Usuario> GetByNome(string id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            return await connection.QueryFirstOrDefaultAsync<Usuario>("SELECT * FROM Pessoa WHERE Id = @NomeCompleto", new { Id = id });
        }
    }

    public async Task<int> Create(Usuario usuario)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            return await connection.ExecuteAsync("INSERT INTO Pessoa (NomeCompleto, DataNascimento, ValorRenda, CPF) VALUES (@NomeCompleto, @DataNascimento, @ValorRenda, @CPF)", usuario);
        }
    }

    public async Task<int> Update(Usuario usuario)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            return await connection.ExecuteAsync("UPDATE Pessoa SET NomeCompleto = @NomeCompleto, DataNascimento = @DataNascimento, ValorRenda = @ValorRenda, CPF = @CPF WHERE Id = @Id", usuario);
        }
    }

    public async Task<int> Delete(int id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            return await connection.ExecuteAsync("DELETE FROM Pessoa WHERE Id = @Id", new { Id = id });
        }
    }
}
