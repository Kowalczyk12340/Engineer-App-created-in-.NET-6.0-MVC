#nullable disable
using System.Data.SqlClient;
using Dapper;
using Microsoft.EntityFrameworkCore;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;

namespace EngineerApplication.ContextStructure.Data.Service
{
  public class SPCall : ISPCall
  {
    private readonly EngineerDbContext _db;
    private static string? ConnectionString = "";

    public SPCall(EngineerDbContext db)
    {
      _db = db;
      ConnectionString = db.Database.GetDbConnection().ConnectionString;
    }

    public async Task<T> ExecuteReturnScalerAsync<T>(string procedureName, DynamicParameters? param = null)
    {
      using SqlConnection sqlCon = new(ConnectionString);
      await sqlCon.OpenAsync();
      return (T)Convert.ChangeType(value: sqlCon.ExecuteScalar<T>(procedureName, param, commandType: System.Data.CommandType.StoredProcedure), typeof(T));
    }

    public async Task ExecuteWithoutReturnAsync(string procedureName, DynamicParameters? param = null)
    {
      using SqlConnection sqlCon = new(ConnectionString);
      await sqlCon.OpenAsync();
      await sqlCon.ExecuteAsync(procedureName, param, commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<T>> ReturnListAsync<T>(string procedureName, DynamicParameters? param = null)
    {
      using SqlConnection sqlCon = new(ConnectionString);
      await sqlCon.OpenAsync();
      return await sqlCon.QueryAsync<T>(procedureName, param, commandType: System.Data.CommandType.StoredProcedure);
    }

    public void Dispose()
    {
      _db.Dispose();
    }
  }
}
