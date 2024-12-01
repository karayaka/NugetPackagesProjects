using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MyBaseRepository.Abstract;

namespace MyBaseRepository.Concrete;

public class BaseUnitOfWork(DbContext context) : IBaseUnitOfWork
{
    public async Task<IDbContextTransaction> BeginTransaction()
    {
        return await context.Database.BeginTransactionAsync();
    }

    public async Task<int> SaveChanges()
    {
        return await context.SaveChangesAsync();
    }
}