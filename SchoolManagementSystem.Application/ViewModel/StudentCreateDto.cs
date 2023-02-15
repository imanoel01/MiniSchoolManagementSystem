
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Application.ViewModel;

public class StudentCreateDto
{
    [Required]
    public string Firstname { get; set; }
    public string Middlename { get; set; }
    [Required]
    public string Lastname { get; set; }
    public DateTime DOB { get; set; }
    public string Address { get; set; }
    [Required]
    public string Phone { get; set; }
    [EmailAddress]
    public string Email { get; set; }
}

public class StudentUpdateDto
{
    [Required]
    public string Id { get; set; }
    [Required]
    public string Firstname { get; set; }
    public string Middlename { get; set; }
    [Required]
    public string Lastname { get; set; }
    public DateTime DOB { get; set; }
    public string Address { get; set; }
    [Required]
    public string Phone { get; set; }
    [EmailAddress]
    public string Email { get; set; }
}

public class StudentReadDto
{
    public string Id { get; set; }
    public string Firstname { get; set; }
    public string Middlename { get; set; }
    public string Lastname { get; set; }
    public string Fullname
    {
        get
        {
            return $"{Firstname} {Lastname} {Middlename}";
        }
    }
    public DateTime DOB { get; set; }
    public string MatricNo { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
}
