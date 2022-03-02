using EngineerApplication.ContextStructure.Data.Repository;
using EngineerApplication.Entities;

namespace EngineerApplication.ContextStructure.Data.Service.Interfaces
{
  public interface IOfferService : IRepository<Offer>
  {
    void Update(Offer offer);
  }
}
