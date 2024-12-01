using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MyBaseRepository.Abstract;
using MyBaseRepository.Enums;
using MyBaseRepository.Models;

namespace MyBaseRepository.Concrete;

public class Repository(DbContext context, Guid? userId):IRepository
{
    private DbSet<T> GetTable<T>() where T : BaseModel
     {
         return context.Set<T>();
     }
     public async Task<T> Add<T>(T _object) where T : BaseModel
     {
         try
         {
             _object.CreatedBy = userId;
             _object.LastModifiedBy = userId;
             await GetTable<T>().AddAsync(_object);
             return _object;
         }
         catch (Exception)
         {
             throw;
         }

     }
     
     public async Task AddRange<T>(ICollection<T> _objectList) where T : BaseModel
     {
         try
         {
             foreach (var item in _objectList)
             {
                 item.CreatedBy = userId;
                 item.LastModifiedBy = userId;
             }
             await context.AddRangeAsync(_objectList);
         }
         catch (Exception)
         {
             throw;
         }
     }

     public bool Any<T>(Expression<Func<T, bool>> where) where T : BaseModel
     {
         try
         {
             return GetQueryable<T>(t=>true,asNoTracking:false).Any(where);
         }
         catch (Exception)
         {
             throw;
         }
     }

     public bool AnyNonDeletedAndActive<T>(Expression<Func<T, bool>> where) where T : BaseModel
     {
         try
         {
             return GetNonDeletedAndActive<T>(t => t.ObjectStatus == ObjectStatus.NonDeleted,asNoTracking:true).Any(where);
         }
         catch (Exception)
         {
             throw;
         }


     }

     public int Count<T>(Expression<Func<T, bool>> expression) where T : BaseModel
     {
         try
         {
             return GetQueryable<T>(t=>true,asNoTracking:true).Count(expression);
         }
         catch (Exception)
         {
             throw;
         }
     }

     public int CountDeletedAndActive<T>(Expression<Func<T, bool>> expression) where T : BaseModel
     {
         try
         {
             return GetNonDeletedAndActive(expression,asNoTracking:true).Count();
         }
         catch (Exception)
         {
             throw;
         }
     }

     public async Task Delete<T>(Guid ID) where T : BaseModel
     {
         try
         {
             await DeleteRange<T>(t=>t.ID==ID);
         }
         catch (Exception)
         {
             throw;
         }
     }

     public T? Delete<T>(T? model) where T : BaseModel
     {
         try
         {

             model.ObjectStatus = ObjectStatus.Deleted;
             model.Status = Status.Passive;
             Update<T>(model);
             return model;
         }
         catch (Exception)
         {
             throw;
         }
     }

     public void DeleteRange<T>(ICollection<T> _objectList) where T : BaseModel
     {
         try
         {
             foreach (var item in _objectList)
             {
                 item.ObjectStatus = ObjectStatus.Deleted;
                 item.Status = Status.Passive;
             }
             UpdateRange(_objectList);
         }
         catch (Exception)
         {
             throw;
         }
     }

     public async Task DeleteRange<T>(Expression<Func<T, bool>> query) where T : BaseModel
     {
         try
         {
             _ = await UpdateProperties(query, setter => setter
             .SetProperty(s=>s.ObjectStatus,ObjectStatus.Deleted)
             .SetProperty(s=>s.Status,Status.Passive)
             .SetProperty(s=>s.LastModifiedOn,DateTime.Now)
             .SetProperty(s=>s.LastModifiedBy,userId));
         }
         catch (Exception)
         {
             throw;
         }
     }

     public void DeleteRange<T>(IQueryable<T> _objectList) where T : BaseModel
     {
         try
         {
             foreach (var item in _objectList)
             {
                 item.ObjectStatus = ObjectStatus.Deleted;
                 item.Status = Status.Passive;
             }
             UpdateRange(_objectList);
         }
         catch (Exception)
         {
             throw;
         }
     }

     public async Task<T?> Find<T>(Expression<Func<T, bool>> where) where T : BaseModel
     {
         try
         {
             return await GetQueryable<T>(t=>true,asNoTracking:true).FirstOrDefaultAsync(where);
         }
         catch (Exception)
         {
             throw;
         }
     }

     public async Task<T?> FindNonDeletedAndActive<T>(Expression<Func<T, bool>> where) where T : BaseModel
     {
         try
         {
             return await GetNonDeletedAndActive<T>(t => true,asNoTracking:true).FirstOrDefaultAsync(where);
         }
         catch (Exception)
         {
             throw;
         }
     }

     public async Task<T?> GetByID<T>(Guid ID) where T : BaseModel
     {
         try
         {
             return await Find<T>(t => t.ID == ID);
         }
         catch (Exception)
         {
             throw;
         }
     }

     public async Task<TResult?> GetByID<T, TResult>(Guid ID, Expression<Func<T, TResult>> selector) where T : BaseModel
     {
         try
         {
             return await GetQueryable<T>(t=>t.ID==ID,asNoTracking:true).Select(selector).FirstOrDefaultAsync();
         }
         catch (Exception)
         {
             throw;
         }
     }

     public IQueryable<T> GetQueryable<T>(Expression<Func<T, bool>> where,bool asNoTracking=false) where T : BaseModel
     {
         try
         {
             var quary = GetTable<T>().Where(where);
             if(asNoTracking)
                 return quary.AsNoTracking();
             return quary;
         }
         catch (Exception)
         {
             throw;
         }
     }

     public IQueryable<T> GetNonDeletedAndActive<T>(Expression<Func<T, bool>> expression, bool asNoTracking = false) where T : BaseModel
     {
         try
         {
             return GetQueryable<T>(t => t.ObjectStatus == ObjectStatus.NonDeleted && t.Status == Status.Active,asNoTracking)
                 .Where(expression);
         }
         catch (Exception)
         {
             throw;
         }
     }
     
     public void RemoveRange<T>(params T[] entities) where T : BaseModel
     {
         try
         {
             GetTable<T>().RemoveRange(entities);
         }
         catch (Exception)
         {
             throw;
         }
     }

     public void RemoveRange<T>(IEnumerable<T> entities) where T : BaseModel
     {
         GetTable<T>().RemoveRange(entities);
     }
     public async Task RemoveRange<T>(Expression<Func<T, bool>> where) where T : BaseModel
     {
         try
         {
             await GetQueryable<T>(where,asNoTracking:true).ExecuteDeleteAsync();
         }
         catch (Exception)
         {

             throw;
         }

     }

     public T Update<T>(T _object) where T : BaseModel
     {
         try
         {
             _object.LastModifiedBy = userId;
             _object.LastModifiedOn = DateTime.Now;
             GetTable<T>().Update(_object);
             return _object;
         }
         catch (Exception)
         {
             throw;
         }
     }

     public void UpdateRange<T>(ICollection<T> _objectList) where T : BaseModel
     {
         try
         {
             foreach (var item in _objectList)
             {
                 item.LastModifiedBy = userId;
                 item.LastModifiedOn = DateTime.Now;
             }
             GetTable<T>().UpdateRange(_objectList);
         }
         catch (Exception)
         {
             throw;
         }
     }

     public void UpdateRange<T>(IQueryable<T> _objectList) where T : BaseModel
     {
         try
         {
             foreach (var item in _objectList)
             {
                 item.LastModifiedBy = userId;
                 item.LastModifiedOn = DateTime.Now;
             }
             GetTable<T>().UpdateRange(_objectList);
         }
         catch (Exception)
         {
             throw;
         }
     }
     public async Task<int> UpdateProperties<T>(Expression<Func<T, bool>> query, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> properties) where T : BaseModel
     {
         return await GetQueryable<T>(query,asNoTracking:true).ExecuteUpdateAsync(properties);
     }

     public Task<int> UpdateProperties<T>(Guid ID, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> properties) where T : BaseModel
     {
         try
         {
             return UpdateProperties<T>(t => t.ID == ID, properties);
         }
         catch (Exception)
         {

             throw;
         }
     }
        
}
