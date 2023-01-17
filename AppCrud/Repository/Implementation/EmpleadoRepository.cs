﻿using AppCrud.Models;
using AppCrud.Repository.Contract;
using System.Data;
using System.Data.SqlClient;

namespace AppCrud.Repository.Implementation;

public class EmpleadoRepository: IGenericRepository<Empleado>
{
    private readonly string _cadenaSQL = "";

    public EmpleadoRepository(IConfiguration configuration)
    {
        _cadenaSQL = configuration.GetConnectionString("cadenaSQL");
    }
    
    public async Task<List<Empleado>> Lista()
    {
        List<Empleado> _lista = new List<Empleado>();
        using (var connection = new SqlConnection(_cadenaSQL))
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("sp_ListaEmpleados", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            using (var dr = await cmd.ExecuteReaderAsync())
            {
                while (await dr.ReadAsync())
                {
                    _lista.Add(new Empleado
                    {
                        idEmpleado = Convert.ToInt32(dr["idEmpleado"]),
                        nombreCompleto = dr["nombreCompleto"].ToString(),
                        refDepartamento = new Departamento
                        {
                            idDepartamento = Convert.ToInt32(dr["idDepartamento"]),
                            nombre = dr["nombre"].ToString()
                        },
                        sueldo = Convert.ToInt32(dr["sueldo"]),
                        fechaContrato = dr["fechaContrato"].ToString() 
                    });
                }
            }
        }

        return _lista;
    }

    public async Task<bool> Guardar(Empleado modelo)
    {
        using (var connection = new SqlConnection(_cadenaSQL))
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("sp_GuardarEmpleado", connection);
            cmd.Parameters.AddWithValue("nombreCompleto", modelo.nombreCompleto);
            cmd.Parameters.AddWithValue("idDepartamento", modelo.refDepartamento);
            cmd.Parameters.AddWithValue("sueldo", modelo.sueldo);
            cmd.Parameters.AddWithValue("fechaContrato", modelo.fechaContrato);
            cmd.CommandType = CommandType.StoredProcedure;

            int affectedRows = await cmd.ExecuteNonQueryAsync();

            if (affectedRows > 0)
                return true;
            else
                return false;
            

        }
    }

    public async Task<bool> Editar(Empleado modelo)
    {
        using (var connection = new SqlConnection(_cadenaSQL))
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("sp_EditarEmpleado", connection);
            cmd.Parameters.AddWithValue("idEmpleado", modelo.idEmpleado);
            cmd.Parameters.AddWithValue("nombreCompleto", modelo.nombreCompleto);
            cmd.Parameters.AddWithValue("idDepartamento", modelo.refDepartamento);
            cmd.Parameters.AddWithValue("sueldo", modelo.sueldo);
            cmd.Parameters.AddWithValue("fechaContrato", modelo.fechaContrato);
            cmd.CommandType = CommandType.StoredProcedure;

            int affectedRows = await cmd.ExecuteNonQueryAsync();

            if (affectedRows > 0)
                return true;
            else
                return false;
            

        }
    }

    public async Task<bool> Eliminar(int id)
    {
        using (var connection = new SqlConnection(_cadenaSQL))
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("sp_EliminarEmpleado", connection);
            cmd.Parameters.AddWithValue("idEmpleado", id);
            cmd.CommandType = CommandType.StoredProcedure;

            int affectedRows = await cmd.ExecuteNonQueryAsync();

            if (affectedRows > 0)
                return true;
            else
                return false;
            

        }
    }
}