using System.Linq.Expressions;

namespace NLayerCleanArchitecture.Repository;
//BU CLASS ŞİŞMEMELİ
public interface IGenericRepository<T> where T: class
{
    //metodun asenkronluğuna tolist , tolistasync kullanılarak service katmanında karar verilecektir
    IQueryable<T> GetAll();
    //Func<T,bool> bildiğimiz delege, sen T ver ben bool döncem diyor, Expresison ise bunu db query'sine çeviriyor
    IQueryable<T> Where(Expression<Func<T, bool>> predicate); 
    ValueTask<T?> GetByIdAsync(int id); //artık valuetype döneriz
    ValueTask AddAsync(T entity);
    void Update(T entity);//unitofwork ile asenkron, update'in asenkron metodu yok
    void Delete(T entity);//unitofwork ile asenkron, update'in asenkron metodu yok, Id alma direkt entity al babba
}