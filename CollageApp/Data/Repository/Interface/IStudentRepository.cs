using CollageApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CollageApp.Data.Repository.Interface
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetStudents();
        Task<Student> GetStudent(int id, bool useNoTracking = false);
        Task<Student> GetStudentByName(string name);
        Task<int> CreateStudent(Student model);
        Task<bool> DeleteStudent(Student student);
        Task<int> UpdateStudent(Student model);
    }
}
