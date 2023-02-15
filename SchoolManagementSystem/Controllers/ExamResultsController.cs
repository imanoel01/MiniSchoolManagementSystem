using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Application.Concrete;
using SchoolManagementSystem.Application.ViewModel;
using SchoolManagementSystem.Common.Models;

namespace SchoolManagementSystem.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class ExamResultsController : ControllerBase
    {
        private readonly IExamService _examService;

        public ExamResultsController(IExamService examService)
        {
            _examService = examService;
        }


        /// <summary>
        /// Endpoint for Posting new result.
        /// 
        /// </summary>
        [HttpPost]
        [Route("submitResult")]

        [ProducesResponseType(typeof(ResponseModel), 200)]
        [ProducesResponseType(typeof(ResponseModel), 400)]
        public async Task<IActionResult> CreateStudent([FromBody] SubmitExamResult model)
        {
            var request = await _examService.SubmitExamResult(model);
            if (request.IsSuccessful)
                return Ok(request);
            return BadRequest(request);
        }


        /// <summary>
        /// Generate Pdf of All Results in the db
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GenerateResult()
        {
            var request = await _examService.GenerateAllResult();
            if (request.IsSuccessful)
            {
                return File(request.Data, System.Net.Mime.MediaTypeNames.Application.Octet, "AllResults" + ".pdf");

            }
            return NotFound(request);
        }
    }
}
