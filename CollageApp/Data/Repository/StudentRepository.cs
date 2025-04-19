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

        public async Task<int> CreateStudent(Student model)
        {
            if (model == null)
            {
                throw new Exception("Model is empty");
            }
            else
            {
                await _dbContext.Students.AddAsync(model);
                await _dbContext.SaveChangesAsync();
            }

            return model.Id;
        }

        public async Task<bool> DeleteStudent(Student student)
        {
            _dbContext.Students.Remove(student);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<Student> GetStudent(int id, bool useNoTracking = false)
        {
            var student = new Student();

            if (useNoTracking == true)
            {
                student = await _dbContext.Students.AsNoTracking().Where(n => n.Id == id).FirstOrDefaultAsync();
            }
            else
            {
                student = await _dbContext.Students.Where(n => n.Id == id).FirstOrDefaultAsync();

            }

            if (student == null)
            {
                throw new Exception("Student not found");
            }

            return student;
        }

        public async Task<Student> GetStudentByName(string name)
        {
            var student = await _dbContext.Students.Where(n => n.StudentName == name).FirstOrDefaultAsync();
            
            if(student == null)
            {
                throw new Exception("Student not found");
            }

            return student;
        }

        public async Task<List<Student>> GetStudents()
        {
            return await _dbContext.Students.ToListAsync();
        }

        public async Task<int> UpdateStudent(Student model)
        {
            _dbContext.Students.Update(model);
            await _dbContext.SaveChangesAsync();

            return model.Id;
        }
    }
}
