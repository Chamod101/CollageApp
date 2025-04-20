using CollageApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CollageApp.Data.Repository.Interface
{
    public interface IStudentRepository : ICommonRepository<Student>
    {
        Task<List<Student>> GetStudentsByAttendence();
    }
}
