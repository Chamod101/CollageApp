using CollageApp.Data.Repository.Interface;
using CollageApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollageApp.Data.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly CollegeDBContext _dbContext;

        public StudentRepository(CollegeDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public StudentDTO CreateStudent(StudentDTO model)
        {
            throw new NotImplementedException();
        }

        public bool DeleteStudent(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Student> GetStudent(int id)
        {
            var student = await _dbContext.Students.Where(n => n.Id == id).FirstOrDefaultAsync();

            if(student == null)
            {
                throw new Exception("Student not found");
            }

            return student;
        }

        public Student GetStudentByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Student>> GetStudents()
        {
            return await _dbContext.Students.ToListAsync();
        }

        public ActionResult UpdateStudent(StudentDTO model)
        {
            throw new NotImplementedException();
        }
    }
}
