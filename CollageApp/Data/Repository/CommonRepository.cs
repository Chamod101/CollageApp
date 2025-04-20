using CollageApp.Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace CollageApp.Data.Repository
{
    public class CommonRepository<T> : ICommonRepository<T> where T : class
    {
        private readonly CollegeDBContext _dbContext;
        private DbSet<T> _dbSet;

        public CommonRepository(CollegeDBContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<T> CreateRecord(T Record)
        {
            if (Record == null)
            {
                throw new Exception("Model is empty");
            }
            else
            {
                _dbSet.Add(Record);
                await _dbContext.SaveChangesAsync();
            }

            return Record;
        }

        public async Task<bool> DeleteRecord(T Record)
        {
            _dbSet.Remove(Record);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<T> GetRecordByFilter(Expression<Func<T, bool>> filter, bool useNoTracking = false)
        {
            T? student;

            if (useNoTracking)
            {
                student = await _dbSet.AsNoTracking().Where(filter).FirstOrDefaultAsync();
            }
            else
            {
                student = await _dbSet.Where(filter).FirstOrDefaultAsync();

            }

            return student ?? throw new InvalidOperationException("No record found");
        }

        //public Task<T> GetRecordByFilter2(Expression<Func<T, bool>> filter)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<List<T>> GetAllRecords()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> UpdateRecord(T Record)
        {
            _dbSet.Update(Record);
            await _dbContext.SaveChangesAsync();

            return Record;
        }
    }
}
