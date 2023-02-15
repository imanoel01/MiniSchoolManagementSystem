using Microsoft.Extensions.DependencyInjection;
using SchoolManagementSystem.Application.Concrete;
using SchoolManagementSystem.Application.Implementation;
using SchoolManagementSystem.Common.Repositories;
using SchoolManagementSystem.Persistence.Reposistories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Application
{
    public static class DependencyInjection
    {

        public static void AddApplicationServices(this IServiceCollection service)
        {
            // Data repositories
            service.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            service.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            service.AddTransient<IStudentService, StudentService>();
            service.AddTransient<ICourseService, CourseService>();
            service.AddTransient<IReportService, ReportService>();
            service.AddTransient<IExamService, ExamService>();
            

            service.AddAutoMapper(Assembly.GetExecutingAssembly());

        }
    }
}
