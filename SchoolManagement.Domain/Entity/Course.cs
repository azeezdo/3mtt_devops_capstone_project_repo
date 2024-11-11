using System;
namespace SchoolManagement.Domain.Entity
{
	public class Course : BaseEntity
	{
        public string CourseName { get; set; }
        public long TeacherId { get; set; }
        public long studentId { get; set; }
        public Teacher Teacher { get; set; }
        public Student Student { get; set; }
    }
}

