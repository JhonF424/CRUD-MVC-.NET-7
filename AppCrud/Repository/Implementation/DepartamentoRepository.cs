﻿using AppCrud.Models;
using AppCrud.Repository.Contract;
using System.Data;
using System.Data.SqlClient;
namespace AppCrud.Repository.Implementation;

public class DepartamentoRepository: IGenericRepository<Departamento>
{
    private readonly string _cadenaSQL = "";

    public DepartamentoRepository(IConfiguration configuration)
    {
        _cadenaSQL = configuration.GetConnectionString("cadenaSQL");
    }

    public async Task<List<Departamento>> Lista()
    {
        List<Departamento> _lista = new List<Departamento>();
        using (var connection = new SqlConnection(_cadenaSQL))
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("sp_ListaDepartamentos", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            using (var dr = await cmd.ExecuteReaderAsync())
            {
                while (await dr.ReadAsync())
                {
                    _lista.Add(new Departamento
                    {
                        idDepartamento = Convert.ToInt32(dr["idDepartamento"]),
                        nombre = dr["nombre"].ToString()
                    });
                }
            }
        }

        return _lista;
    }
    
    
    /*
     * TODO:
     * Implement stored procedures for this actions in SQL Server
     * 
     */

    public Task<bool> Guardar(Departamento modelo)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Editar(Departamento modelo)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Eliminar(int id)
    {
        throw new NotImplementedException();
    }
}