using Dapper;

namespace EngineerApplication.ContextStructure.Data.Service.Interfaces
{
  public interface ISP_Call : IDisposable
  {
    Task<IEnumerable<T>> ReturnListAsync<T>(string procedureName, DynamicParameters? param = null);

    Task ExecuteWithoutReturnAsync(string procedureName, DynamicParameters? param = null);

    Task<T> ExecuteReturnScalerAsync<T>(string procedureName, DynamicParameters? param = null);
  }
}
