
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Common.Models;

namespace SchoolManagementSystem.Domain.Entities
{

    [Index(nameof(Phone),IsUnique=true)]
    [Index(nameof(Email),IsUnique=true)]
    [Index(nameof(Firstname))]
    [Index(nameof(Middlename))]
    [Index(nameof(Lastname))]
    [Index(nameof(Lastname),nameof(Firstname),nameof(Middlename))]
    public class Student : BaseDeletableModel<string>
    {
        public Student()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Firstname { get; set; }
        public string? Middlename { get; set; }
        public string Lastname { get; set; }
        public DateTime DOB { get; set; }
        public string? Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}