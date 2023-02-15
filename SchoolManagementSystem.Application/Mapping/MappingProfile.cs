using AutoMapper;
using SchoolManagementSystem.Application.ViewModel;
using SchoolManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {

        Config();
        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
    }

    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var types = assembly.GetExportedTypes()
            .Where(t => t.GetInterfaces().Any(i =>
            i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
            .ToList();

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);

            var methodInfo = type.GetMethod("Mapping")
                ?? type.GetInterface("IMapFrom`1").GetMethod("Mapping");

            methodInfo?.Invoke(instance, new object[] { this });
        }

    }
    private void Config()
    {
        CreateMap<StudentCreateDto, Student>().ReverseMap();
        CreateMap<StudentUpdateDto, Student>().ReverseMap();
        CreateMap<StudentReadDto, Student>().ReverseMap();
        CreateMap<CourseCreateDto, Course>().ReverseMap();
        CreateMap<CourseReadDto, Course>().ReverseMap();
    }

}