using System;
namespace SchoolManagement.Domain.Entity
{
	public class Teacher : BaseEntity
	{
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}

