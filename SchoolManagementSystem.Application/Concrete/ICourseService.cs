using SchoolManagementSystem.Application.ViewModel;
using SchoolManagementSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Application.Concrete
{
    public interface ICourseService
    {
        Task<ResponseModel<CourseReadDto>> CreateCourse(CourseCreateDto request);
        Task<ResponseModel<List<CourseReadDto>>> GetAllCourses();
        Task<ResponseModel<CourseReadDto>> GetCourseById(string courseId);
    }
}
