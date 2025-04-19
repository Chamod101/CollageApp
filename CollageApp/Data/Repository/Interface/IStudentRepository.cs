using CollageApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CollageApp.Data.Repository.Interface
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetStudents();
        Task<Student> GetStudent(int id);
        Student GetStudentByName(string name);
        StudentDTO CreateStudent(StudentDTO model);
        bool DeleteStudent(int id);
        ActionResult UpdateStudent(StudentDTO model);
    }
}
