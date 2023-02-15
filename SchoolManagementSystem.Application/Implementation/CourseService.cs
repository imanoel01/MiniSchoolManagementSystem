using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Application.Concrete;
using SchoolManagementSystem.Application.ViewModel;
using SchoolManagementSystem.Common.Models;
using SchoolManagementSystem.Common.Repositories;
using SchoolManagementSystem.Domain.Entities;
using Serilog;


namespace SchoolManagementSystem.Application.Implementation;

public class CourseService :ICourseService
{
    private readonly IDeletableEntityRepository<Course> _courseRepo;
    private readonly IMapper _mapper;

    public CourseService(IDeletableEntityRepository<Course> courseRepository, IMapper mapper)
    {
        _courseRepo = courseRepository;
        _mapper = mapper;
    }


    public async Task<ResponseModel<CourseReadDto>> CreateCourse(CourseCreateDto request)
    {
        try
        {
            request.Name = request.Name.ToUpper();
            request.Code = request.Code.ToUpper();

            var isCourseExist = await _courseRepo.AllAsNoTracking().SingleOrDefaultAsync(x => x.Name == request.Name || x.Code == request.Code);
            if (isCourseExist != null)
            {
                return ResponseModel<CourseReadDto>.Failure($"Name or Course Code  already exists");

            }
            var createCourse = _mapper.Map<Course>(request);
           
            await _courseRepo.AddAsync(createCourse);
            await _courseRepo.SaveChangesAsync();

            var readCourse = _mapper.Map<CourseReadDto>(createCourse);
            return ResponseModel<CourseReadDto>.Success(readCourse);

        }
        catch (Exception ex)
        {

            Log.Error($"Exception occured while saving Course: {ex.Message}", ex);
            return ResponseModel<CourseReadDto>.Failure("Exception error");

        }

    }
    public async Task<ResponseModel<CourseReadDto>> GetCourseById(string courseId)
    {
        try
        {
            var isCourseExist = await _courseRepo.AllAsNoTracking().SingleOrDefaultAsync(x => x.Id == courseId);
            if (isCourseExist == null)
            {
                return ResponseModel<CourseReadDto>.Failure($"Course Id Not Found");
            }

            var readStudent = _mapper.Map<CourseReadDto>(isCourseExist);
            return ResponseModel<CourseReadDto>.Success(readStudent);

        }
        catch (Exception ex)
        {
            Log.Error($"Exception occured while retrieving Course: {ex.Message}", ex);
            return ResponseModel<CourseReadDto>.Failure("Exception error");

        }
    }

    public async Task<ResponseModel<List<CourseReadDto>>> GetAllCourses()
    {
        try
        {
            var isCourseExist = await _courseRepo.AllAsNoTracking().ToListAsync();

            var readCourse = _mapper.Map<List<CourseReadDto>>(isCourseExist);
            return ResponseModel<List<CourseReadDto>>.Success(readCourse);

        }
        catch (Exception ex)
        {
            Log.Error($"Exception occured while retrieving Course: {ex.Message}", ex);
            return ResponseModel<List<CourseReadDto>>.Failure("Exception error");

        }
    }
}
