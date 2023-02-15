using SchoolManagementSystem.Application.ViewModel;
using SchoolManagementSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Application.Concrete
{
    public interface IStudentService
    {
        Task<ResponseModel<StudentReadDto>> CreateStudent(StudentCreateDto request);
        Task<ResponseModel<List<StudentReadDto>>> GetAllStudents();
        Task<ResponseModel<StudentReadDto>> GetStudentById(string studentId);
        Task<ResponseModel<StudentReadDto>> UpdateStudent(StudentUpdateDto request);
    }
}
