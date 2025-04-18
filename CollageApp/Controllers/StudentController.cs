using CollageApp.Data;
using CollageApp.Models;
using CollageApp.MyLogging;
using Microsoft.AspNetCore.Mvc;

namespace CollageApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMyLogger _myLogger;
        private readonly CollegeDBContext _dbContext;

        public StudentController(IMyLogger MyLogger, CollegeDBContext dbContext)
        {
            _myLogger = MyLogger;
            _dbContext = dbContext;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<StudentDTO>> GetStudents()
        {
            _myLogger.Log("Get Students started");
            var students = _dbContext.Students.Select(s => new StudentDTO
            {
                Id = s.Id,
                StudentName = s.StudentName,
                Email = s.Email,
                Address = s.Address,
            });
            return Ok(students);

        }

        [HttpGet("{id:int}")]
        public ActionResult<Student> GetStudent(int id)
        {
            return Ok(_dbContext.Students.Where(n=>n.Id==id).FirstOrDefault());
        }

        [HttpGet("{name}")]
        public ActionResult<Student> GetStudentByName(string name)
        {
            return Ok(_dbContext.Students.Where(n => n.StudentName == name).FirstOrDefault());
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> CreateStudent([FromBody]StudentDTO model)
        {
            if (model == null) return BadRequest();

            
            Student student = new Student()
            {
     
                StudentName = model.StudentName,
                Email = model.Email,
                Address = model.Address,
            };

            _dbContext.Students.Add(student);
            _dbContext.SaveChanges();
            return Ok(model);
            //return CreatedAtRoute("GetStudent", new { id = model.Id }, model);

        }

        [HttpDelete("{id:int}")] 
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<bool> DeleteStudent(int id)
        {
            if (id <= 0) return BadRequest();

            var student = _dbContext.Students.Where(n=> n.Id==id).FirstOrDefault();
            if (student == null) return NotFound();

            _dbContext.Students.Remove(student);
            _dbContext.SaveChanges();
            return Ok(true);
        }


        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult UpdateStudent([FromBody]StudentDTO model)
        {
            if(model == null)
                return BadRequest();

            var exsistingUser = _dbContext.Students.Where(s => s.Id == model.Id).FirstOrDefault();

            if(exsistingUser == null)
            {
                return NotFound();
            }
            else
            {
                exsistingUser.StudentName = model.StudentName;
                exsistingUser.Address = model.Address;
                exsistingUser.Email = model.Email;
            }

            return NoContent();
        }
    } 
}
