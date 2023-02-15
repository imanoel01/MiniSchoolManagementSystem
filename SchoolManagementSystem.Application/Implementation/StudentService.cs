using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Application.Concrete;
using SchoolManagementSystem.Application.ViewModel;
using SchoolManagementSystem.Common.Models;
using SchoolManagementSystem.Common.Repositories;
using SchoolManagementSystem.Domain.Entities;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Application.Implementation
{
    public class StudentService : IStudentService
    {
        private readonly IDeletableEntityRepository<Student> _studentRepo;
        private readonly IMapper _mapper;

        public StudentService(IDeletableEntityRepository<Student> studentRepository, IMapper mapper)
        {
            _studentRepo = studentRepository;
            _mapper = mapper;
        }

        public async Task<ResponseModel<StudentReadDto>> CreateStudent(StudentCreateDto request)
        {
            try
            {
                var isStudentExist = await _studentRepo.AllAsNoTracking().SingleOrDefaultAsync(x => x.Email.ToUpper() == request.Email.ToUpper() || x.Phone == request.Phone);
                if (isStudentExist != null)
                {
                    return ResponseModel<StudentReadDto>.Failure($"Email or Phone Number  already exists");

                }
                var createStudent = _mapper.Map<Student>(request);
                await _studentRepo.AddAsync(createStudent);
                await _studentRepo.SaveChangesAsync();

                var readStudent = _mapper.Map<StudentReadDto>(createStudent);
                return ResponseModel<StudentReadDto>.Success(readStudent);

            }
            catch (Exception ex)
            {

                Log.Error($"Exception occured while saving student: {ex.Message}", ex);
                return ResponseModel<StudentReadDto>.Failure("Exception error");

            }

        }

        public async Task<ResponseModel<StudentReadDto>> UpdateStudent(StudentUpdateDto request)
        {
            try
            {
                var isStudentExist = await _studentRepo.AllAsNoTracking().SingleOrDefaultAsync(x => x.Id == request.Id);
                if (isStudentExist == null)
                {
                    return ResponseModel<StudentReadDto>.Failure($"StudentId Not Found");

                }

                var emailPhoneExisit = await _studentRepo.AllAsNoTracking().SingleOrDefaultAsync(x => x.Id != request.Id && x.Email.ToUpper() == request.Email.ToUpper() || x.Phone == request.Phone);

                if (emailPhoneExisit != null)
                {
                    return ResponseModel<StudentReadDto>.Failure($"Email or Phone Number  already exists");

                }

                var updateStudent = _mapper.Map<Student>(request);
                _studentRepo.Update(updateStudent);
                await _studentRepo.SaveChangesAsync();

                var readStudent = _mapper.Map<StudentReadDto>(updateStudent);
                return ResponseModel<StudentReadDto>.Success(readStudent);

            }
            catch (Exception ex)
            {
                Log.Error($"Exception occured while updating student: {ex.Message}", ex);
                return ResponseModel<StudentReadDto>.Failure("Exception error");

            }
        }


        public async Task<ResponseModel<StudentReadDto>> GetStudentById(string studentId)
        {
            try
            {
                var isStudentExist = await _studentRepo.AllAsNoTracking().SingleOrDefaultAsync(x => x.Id == studentId);
                if (isStudentExist == null)
                {
                    return ResponseModel<StudentReadDto>.Failure($"Student Id Not Found");
                }

                var readStudent = _mapper.Map<StudentReadDto>(isStudentExist);
                return ResponseModel<StudentReadDto>.Success(readStudent);

            }
            catch (Exception ex)
            {
                Log.Error($"Exception occured while retrieving student: {ex.Message}", ex);
                return ResponseModel<StudentReadDto>.Failure("Exception error");

            }
        }

        public async Task<ResponseModel<List<StudentReadDto>>> GetAllStudents()
        {
            try
            {
                var isStudentExist = await _studentRepo.AllAsNoTracking().ToListAsync();

                var readStudent = _mapper.Map<List<StudentReadDto>>(isStudentExist);
                return ResponseModel<List<StudentReadDto>>.Success(readStudent);

            }
            catch (Exception ex)
            {
                Log.Error($"Exception occured while retrieving student: {ex.Message}", ex);
                return ResponseModel<List<StudentReadDto>>.Failure("Exception error");

            }
        }
    }
}
