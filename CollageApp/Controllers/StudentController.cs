using CollageApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CollageApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Student>> GetStudents()
        {
            return Ok(StudentsRepository.Students);
        }

        [HttpGet("{id:int}")]
        public ActionResult<Student> GetStudent(int id)
        {
            return Ok(StudentsRepository.Students.Where(n=>n.Id==id).FirstOrDefault());
        }

        [HttpGet("{name}")]
        public ActionResult<Student> GetStudentByName(string name)
        {
            return Ok(StudentsRepository.Students.Where(n => n.StudentName == name).FirstOrDefault());
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<bool> DeleteStudent(int id)
        {
            if (id <= 0) return BadRequest();

            var student = StudentsRepository.Students.Where(n=> n.Id==id).FirstOrDefault();
            if (student == null) return NotFound();

            StudentsRepository.Students.Remove(student);
            return Ok(true);
        }
    }
}
