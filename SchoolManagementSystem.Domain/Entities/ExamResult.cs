
using SchoolManagementSystem.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Domain.Entities;

public class ExamResult :BaseDeletableModel<string>
{
    public ExamResult()
    {
        Id = Guid.NewGuid().ToString();
    }
    public string StudentId { get; set; }
    public Student Student { get; set; }
    public string CourseId { get; set; }
    public Course Course { get; set; }
    [Range(0,100)]
    public int Score { get; set; }

}
