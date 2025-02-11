using CollageApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CollageApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<StudentDTO>> GetStudents()
        {
            var students = StudentsRepository.Students.Select(s => new StudentDTO
            {
                Id = s.Id,
                StudentName = s.StudentName,
                Email = s.Email,
                Address = s.Address,
            });
            return Ok(students);

            //var students = new List<StudentDTO>();
            //foreach(var item in StudentsRepository.Students)
            //{
            //    StudentDTO Obj = new StudentDTO()
            //    {
            //        Id = item.Id,
            //        StudentName = item.StudentName,
            //        Email = item.Email,
            //        Address = item.Address,
            //    };

            //    students.Add(Obj);
            //}

            //return Ok(students);
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

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> CreateStudent([FromBody]StudentDTO model)
        {
            if (model == null) return BadRequest();

            int id = StudentsRepository.Students.LastOrDefault().Id + 1;
            Student student = new Student()
            {
                Id = id,
                StudentName = model.StudentName,
                Email = model.Email,
                Address = model.Address,
            };

            StudentsRepository.Students.Add(student);
            model.Id = id;
            return Ok(model);

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
