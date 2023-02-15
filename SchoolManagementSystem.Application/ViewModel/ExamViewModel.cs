
using System.ComponentModel.DataAnnotations;
namespace SchoolManagementSystem.Application.ViewModel;

public class SubmitExamResult
{
    public string CourseId { get; set; }
    public List<StudenIds> StudentScores { get; set; }
}

public class StudenIds
{
    public string StudentId { get; set; }
    [Range(0, 100)]
    public int Score { get; set; }
}


public class CreateExamResult
{
    public string StudentId { get; set; }
    public string CourseId { get; set; }
    public int Score { get; set; }
}