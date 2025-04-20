using System.Linq.Expressions;

namespace CollageApp.Data.Repository.Interface
{
    public interface ICommonRepository<T>
    {
        Task<List<T>> GetAllRecords();
        Task<T> GetRecordByFilter(Expression<Func<T,bool>> filter, bool useNoTracking = false);
        //Task<T> GetRecordByFilter2(Expression<Func<T, bool>> filter);
        Task<T> CreateRecord(T Record);
        Task<bool> DeleteRecord(T Record);
        Task<T> UpdateRecord(T Record);
    }
}
