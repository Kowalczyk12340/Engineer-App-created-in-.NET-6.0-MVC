using System.Data.SqlClient;
using Dapper;
using Microsoft.EntityFrameworkCore;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;

namespace EngineerApplication.ContextStructure.Data.Service
{
  public class SP_Call : ISP_Call
  {
    private readonly EngineerDbContext _db;
    private static string ConnectionString = "";

    public SP_Call(EngineerDbContext db)
    {
      _db = db;
      ConnectionString = db.Database.GetDbConnection().ConnectionString;
    }

    public T ExecuteReturnScaler<T>(string procedureName, DynamicParameters? param = null)
    {
      using SqlConnection sqlCon = new(ConnectionString);
      sqlCon.Open();
      return (T)Convert.ChangeType(value: sqlCon.ExecuteScalar<T>(procedureName, param, commandType: System.Data.CommandType.StoredProcedure), typeof(T));
    }

    public void ExecuteWithoutReturn(string procedureName, DynamicParameters? param = null)
    {
      using SqlConnection sqlCon = new(ConnectionString);
      sqlCon.Open();
      sqlCon.Execute(procedureName, param, commandType: System.Data.CommandType.StoredProcedure);
    }

    public IEnumerable<T> ReturnList<T>(string procedureName, DynamicParameters? param = null)
    {
      using SqlConnection sqlCon = new(ConnectionString);
      sqlCon.Open();
      return sqlCon.Query<T>(procedureName, param, commandType: System.Data.CommandType.StoredProcedure);
    }

    public void Dispose()
    {
      _db.Dispose();
    }
  }
}
