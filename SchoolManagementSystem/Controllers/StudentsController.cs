using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Application.Concrete;
using SchoolManagementSystem.Application.ViewModel;
using SchoolManagementSystem.Common.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SchoolManagementSystem.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }


        /// <summary>
        /// Get Student By Id
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        [HttpGet("{studentId}")]
        [ProducesResponseType(typeof(ResponseModel<StudentReadDto>), 200)]
        [ProducesResponseType(typeof(ResponseModel), 400)]
        public async Task<IActionResult> Get(string studentId)
        {
            var request = await _studentService.GetStudentById(studentId);
            if (request.IsSuccessful)
                return Ok(request);
            return NotFound(request);
        }

        /// <summary>
        /// Get All Students
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ResponseModel<List<StudentReadDto>>), 200)]
        [ProducesResponseType(typeof(ResponseModel), 400)]
        public async Task<IActionResult> GetAllStudents()
        {
            return Ok(await _studentService.GetAllStudents());
        }


        /// <summary>
        /// Create Student
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("createStudent")]

        [ProducesResponseType(typeof(ResponseModel<StudentReadDto>), 201)]
        [ProducesResponseType(typeof(ResponseModel), 400)]
        public async Task<IActionResult> CreateStudent([FromBody] StudentCreateDto model)
        {
            var request = await _studentService.CreateStudent(model);
            if (request.IsSuccessful)
                return Created(request.Data.Id, request);
            return BadRequest(request);
        }

        /// <summary>
        /// Update Student
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("updateStudent")]
        [ProducesResponseType(typeof(ResponseModel<StudentReadDto>), 200)]
        [ProducesResponseType(typeof(ResponseModel), 400)]
        public async Task<IActionResult> UpdateStudent([FromBody] StudentUpdateDto model)
        {
            var request = await _studentService.UpdateStudent(model);
            if (request.IsSuccessful)
                return Ok(request);
            return BadRequest(request);
        }

        // DELETE api/<StudentsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
