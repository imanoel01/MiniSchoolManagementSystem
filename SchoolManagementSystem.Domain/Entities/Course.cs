using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Common.Models;

namespace SchoolManagementSystem.Domain.Entities;

[Index(nameof(Name),IsUnique=true)]
[Index(nameof(Code),IsUnique=true)]
public class Course : BaseDeletableModel<string>
{
    public Course()
    {
        Id = Guid.NewGuid().ToString();
    }

    public string Name { get; set; }
    public string Code { get; set; }
    public string?  Description { get; set; }
}
