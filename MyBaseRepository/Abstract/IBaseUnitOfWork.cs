using Microsoft.EntityFrameworkCore.Storage;

namespace MyBaseRepository.Abstract;

public interface IBaseUnitOfWork
{
    /// <summary>
    /// Transaction başlatn method
    /// </summary>
    /// <returns></returns>
    Task<IDbContextTransaction> BeginTransaction();
    /// <summary>
    /// Save change işlemleri yapan mehod
    /// </summary>
    /// <returns></returns>
    Task<int> SaveChanges();
}
