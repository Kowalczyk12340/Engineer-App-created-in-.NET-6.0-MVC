using Dapper;

namespace EngineerApplication.ContextStructure.Data.Service.Interfaces
{
  public interface ISP_Call : IDisposable
  {
    IEnumerable<T> ReturnList<T>(string procedureName, DynamicParameters? param = null);

    void ExecuteWithoutReturn(string procedureName, DynamicParameters? param = null);

    T ExecuteReturnScaler<T>(string procedureName, DynamicParameters? param = null);
  }
}
