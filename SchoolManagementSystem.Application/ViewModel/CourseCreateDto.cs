

using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Application.ViewModel;

public class CourseCreateDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Code { get; set; }
    public string? Description { get; set; }
}

public class CourseReadDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string? Description { get; set; }
}

