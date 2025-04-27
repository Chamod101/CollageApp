using AutoMapper;
using CollageApp.Data;
using CollageApp.Data.Repository;
using CollageApp.Data.Repository.Interface;
using CollageApp.Models;
using CollageApp.MyLogging;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CollageApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors(PolicyName = "AllowOnlyGoogleApplications")]
    public class StudentController : ControllerBase
    {
        private readonly IMyLogger _myLogger;
        private readonly IMapper _mapper;
        //private readonly IStudentRepository _studentRepository;
        private readonly IStudentRepository _studentRepository;

        public StudentController(IMyLogger MyLogger, IMapper mapper, IStudentRepository studentRepository)
        {
            _myLogger = MyLogger;
            _mapper = mapper;
            _studentRepository = studentRepository;
        }

        [HttpGet]
        [Route("GetAllStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<StudentDTO>>> GetStudents()
        {

            //var students = await _studentRepository.GetStudents();
            var students = await _studentRepository.GetAllRecords();

            var StudentDtoData = _mapper.Map<List<StudentDTO>>(students);
            
            return Ok(StudentDtoData);

        }

        [DisableCors]
        [HttpGet("GetStudentByID/{id:int}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            //var result = await _studentRepository.GetStudent(id);
            var result = await _studentRepository.GetRecordByFilter(student => student.Id == id);
            return Ok(result);
        }

        //[HttpGet("GetStudentByName/{name}")]
        //public async Task<ActionResult<Student>> GetStudentByName(string name)
        //{
        //    var result = await _studentRepository.GetStudentByName(name);
        //    return Ok(result);
        //}

        [HttpPost]
        [Route("CreateStudent")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StudentDTO>> CreateStudent([FromBody] StudentDTO model)
        {
            if (model == null) return BadRequest();


            Student student = _mapper.Map<Student>(model);


            //var id = await _studentRepository.CreateStudent(student);
            var result = await _studentRepository.CreateRecord(student);
            model.Id = result.Id;
            return Ok(model);
            //return CreatedAtRoute("GetStudent", new { id = model.Id }, model);

        }

        [HttpDelete("DeleteStudent/{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> DeleteStudent(int id)
        {
            if (id <= 0) return BadRequest();

            //var student = await _studentRepository.GetStudent(id);
            var student = await _studentRepository.GetRecordByFilter(student => student.Id == id);
            if (student == null) return NotFound($"The student with id {id} not found");

            //await _studentRepository.DeleteStudent(student);
            await _studentRepository.DeleteRecord(student);
            return Ok(true);
        }


        [HttpPut]
        [Route("UpdateStudent")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateStudent([FromBody] StudentDTO model)
        {
            if (model == null)
                return BadRequest();

            //var exsistingUser = await _studentRepository.GetStudent(model.Id,true);
            var exsistingUser = await _studentRepository.GetRecordByFilter(student => student.Id == model.Id, true);

            if (exsistingUser == null)
            {
                return NotFound();
            }
            
            var newRecord = _mapper.Map<Student>(model);

            //await _studentRepository.UpdateStudent(newRecord);
            await _studentRepository.UpdateRecord(newRecord);

            return NoContent();
        }

        [HttpGet]
        [Route("GetStudentsByAttendence")]
        public async Task<ActionResult<List<Student>>> GetStudentsByAttendence()
        {
            var students = await _studentRepository.GetStudentsByAttendence();
            return Ok(students);
        }
    } 
}
