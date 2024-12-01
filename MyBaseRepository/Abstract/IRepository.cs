using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using MyBaseRepository.Models;

namespace MyBaseRepository.Abstract;

public interface IRepository
{
    /// <summary>
    /// Ekleme işlemi
    /// </summary>
    /// <param name="_object"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    Task<T> Add<T>(T _object) where T : BaseModel;

    /// <summary>
    /// Toplu kleme işlemi
    /// </summary>
    /// <param name="_objectList"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    Task AddRange<T>(ICollection<T> _objectList) where T : BaseModel;

    /// <summary>
    /// Varmu sorgusu
    /// </summary>
    /// <param name="where"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    bool Any<T>(Expression<Func<T, bool>> where) where T : BaseModel;

    /// <summary>
    /// varmı sorgusu
    /// </summary>
    /// <param name="where"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    bool AnyNonDeletedAndActive<T>(Expression<Func<T, bool>> where) where T : BaseModel;

    /// <summary>
    /// kayıt sayısı sorgusu
    /// </summary>
    /// <param name="expression"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    int Count<T>(Expression<Func<T, bool>> expression) where T : BaseModel;

    /// <summary>
    /// Kayıt sayısı sorgusu
    /// </summary>
    /// <param name="expression"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    int CountDeletedAndActive<T>(Expression<Func<T, bool>> expression) where T : BaseModel;
    /// <summary>
    /// Silme işlemi
    /// </summary>
    /// <param name="ID"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    Task Delete<T>(Guid ID) where T : BaseModel;
    /// <summary>
    /// silme işlemi
    /// </summary>
    /// <param name="model"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    T? Delete<T>(T? model) where T : BaseModel;

    /// <summary>
    /// toplu silme işlemi
    /// </summary>
    /// <param name="_objectList"></param>
    /// <typeparam name="T"></typeparam>
    void DeleteRange<T>(ICollection<T> _objectList) where T : BaseModel;

    /// <summary>
    /// toplu silme işlemi
    /// </summary>
    /// <param name="_objectList"></param>
    /// <typeparam name="T"></typeparam>
    void DeleteRange<T>(IQueryable<T> _objectList) where T : BaseModel;

    /// <summary>
    /// Kaytı sorgula
    /// </summary>
    /// <param name="where"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    Task<T?> Find<T>(Expression<Func<T, bool>> where) where T : BaseModel;

    /// <summary>
    /// tekil kayıt sorgula
    /// </summary>
    /// <param name="where"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    Task<T?> FindNonDeletedAndActive<T>(Expression<Func<T, bool>> where) where T : BaseModel;
    /// <summary>
    /// kayıt bu Id ile
    /// </summary>
    /// <param name="ID"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    Task<T?> GetByID<T>(Guid ID) where T : BaseModel;
    /// <summary>
    /// tekil kayıt sorgula se select yap
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="selector"></param>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
    Task<TResult?> GetByID<T, TResult>(Guid ID, Expression<Func<T, TResult>> selector) where T : BaseModel;
    /// <summary>
    /// Sorgula
    /// </summary>
    /// <param name="where"></param>
    /// <param name="asNoTracking"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    IQueryable<T> GetQueryable<T>(Expression<Func<T, bool>> where, bool asNoTracking = false) where T : BaseModel;

    /// <summary>
    /// Sorgula
    /// </summary>
    /// <param name="expression"></param>
    /// <param name="asNoTracking"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    IQueryable<T> GetNonDeletedAndActive<T>(Expression<Func<T, bool>> expression, bool asNoTracking = false)
        where T : BaseModel;
    /// <summary>
    /// Kalıcı silme işlemi
    /// </summary>
    /// <param name="entities"></param>
    /// <typeparam name="T"></typeparam>
    void RemoveRange<T>(params T[] entities) where T : BaseModel;
    /// <summary>
    /// Kalıcı silme işlemi
    /// </summary>
    /// <param name="entities"></param>
    /// <typeparam name="T"></typeparam>
    void RemoveRange<T>(IEnumerable<T> entities) where T : BaseModel;
    /// <summary>
    /// Update kayıt
    /// </summary>
    /// <param name="_object"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    T Update<T>(T _object) where T : BaseModel;

    /// <summary>
    /// toplu kayıt güncelle
    /// </summary>
    /// <param name="_objectList"></param>
    /// <typeparam name="T"></typeparam>
    void UpdateRange<T>(ICollection<T> _objectList) where T : BaseModel;
    /// <summary>
    /// toplu guncelle
    /// </summary>
    /// <param name="_objectList"></param>
    /// <typeparam name="T"></typeparam>
    void UpdateRange<T>(IQueryable<T> _objectList) where T : BaseModel;
    /// <summary>
    /// Propertileri update etme metodu
    /// </summary>
    /// <param name="query"></param>
    /// <param name="properties"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    Task<int> UpdateProperties<T>(Expression<Func<T, bool>> query,
        Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> properties) where T : BaseModel;

    /// <summary>
    /// Propertileri update etme metodu
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="properties"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    Task<int> UpdateProperties<T>(Guid ID, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> properties)
        where T : BaseModel;

}
