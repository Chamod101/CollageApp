using CollageApp.Data.Repository.Interface;
using CollageApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollageApp.Data.Repository
{
    public class StudentRepository : CommonRepository<Student>,IStudentRepository
    {
        private readonly CollegeDBContext _dbContext;

        public StudentRepository(CollegeDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Student>> GetStudentsByAttendence()
        {
            throw new NotImplementedException();
        }
    }
}
