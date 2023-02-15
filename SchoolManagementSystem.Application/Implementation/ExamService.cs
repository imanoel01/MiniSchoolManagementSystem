using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Report.Datas;
using SchoolManagementSystem.Application.Concrete;
using SchoolManagementSystem.Application.ViewModel;
using SchoolManagementSystem.Common.Models;
using SchoolManagementSystem.Common.Repositories;
using SchoolManagementSystem.Domain.Entities;
using Serilog;

namespace SchoolManagementSystem.Application.Implementation;

public class ExamService : IExamService
{
    private readonly IDeletableEntityRepository<ExamResult> _examRepo;
    private readonly IDeletableEntityRepository<Student> _studenRepo;
    private readonly IDeletableEntityRepository<Course> _courseRepo;
    private readonly IReportService _reportService;

    public ExamService(IDeletableEntityRepository<ExamResult> examRepository, IDeletableEntityRepository<Course> courseRepository, IDeletableEntityRepository<Student> studentRepository, IMapper mapper, IReportService reportService)
    {
        _examRepo = examRepository;
        _studenRepo = studentRepository;
        _courseRepo = courseRepository;
        _reportService = reportService;
    }


    public async Task<ResponseModel> SubmitExamResult(SubmitExamResult request)
    {
        try
        {
            List<ExamResult> result = new List<ExamResult>();
            //check that course exist
            var isCourseExist = await _courseRepo.AllAsNoTracking().SingleOrDefaultAsync(x => x.Id == request.CourseId);
            if (isCourseExist == null)
            {
                return ResponseModel.Failure($"Code Id   does not exists");

            }

            foreach (var student in request.StudentScores)
            {
                //check that student exist
                var isStudentIdExist = await _studenRepo.AllAsNoTracking().SingleOrDefaultAsync(x => x.Id == student.StudentId);
                if (isStudentIdExist == null)
                {
                    return ResponseModel.Failure($"Student Id:{student.StudentId} does not exists");
                }

                ExamResult studentScore = new ExamResult
                {
                    CourseId = request.CourseId,
                    StudentId = student.StudentId,
                    Score = student.Score
                };
                await _examRepo.AddAsync(studentScore);

            }

            await _courseRepo.SaveChangesAsync();
            return ResponseModel.Success("Result Saved Successfully");
        }

        catch (Exception ex)
        {

            Log.Error($"Exception occured while saving Course: {ex.Message}", ex);
            return ResponseModel.Failure("Exception error");

        }

    }

    public async Task<ResponseModel<byte[]>> GenerateAllResult()
    {
        try
        {
            var results = await _examRepo.AllAsNoTracking().Select(x =>
            new ExamResultsData
            {
                Name = $"{x.Student.Firstname} {x.Student.Lastname} {x.Student.Middlename}",
                Course = x.Course.Name,
                Score = x.Score
            }

            ).ToListAsync();

            if (results.Count > 0)
            {
                var generatedReport = _reportService.GenerateReportAsync(results);
                return ResponseModel<byte[]>.Success(generatedReport);
            }
            return ResponseModel<byte[]>.Failure("No Result Found");


        }
        catch (Exception ex)
        {
            Log.Error($"Exception occured while generating Result: {ex.Message}", ex);
            return ResponseModel<byte[]>.Failure("Exception error");

        }
    }


}

