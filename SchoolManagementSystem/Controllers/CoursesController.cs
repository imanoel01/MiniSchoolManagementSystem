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
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        /// <summary>
        /// Get Course By Id
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [HttpGet("{courseId}")]
        [ProducesResponseType(typeof(ResponseModel<CourseReadDto>), 200)]
        [ProducesResponseType(typeof(ResponseModel), 400)]
        public async Task<IActionResult> GetCourseById(string courseId)
        {
            var request = await _courseService.GetCourseById(courseId);
            if (request.IsSuccessful)
                return Ok(request);
            return NotFound(request);
        }

        /// <summary>
        /// Get All Courses
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ResponseModel<List<CourseReadDto>>), 200)]
        [ProducesResponseType(typeof(ResponseModel), 400)]
        public async Task<IActionResult> GetAllCourses()
        {
            return Ok(await _courseService.GetAllCourses());
        }


        /// <summary>
        /// Create Course
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("createcourse")]

        [ProducesResponseType(typeof(ResponseModel<CourseReadDto>), 201)]
        [ProducesResponseType(typeof(ResponseModel), 400)]
        public async Task<IActionResult> CreateStudent([FromBody] CourseCreateDto model)
        {
            var request = await _courseService.CreateCourse(model);
            if (request.IsSuccessful)
                return Created(request.Data.Id, request);
            return BadRequest(request);
        }

    }
}
