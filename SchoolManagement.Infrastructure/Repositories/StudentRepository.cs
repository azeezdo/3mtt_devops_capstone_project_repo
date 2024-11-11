using System;
using SchoolManagement.Domain.Entity;
using SchoolManagement.Domain.Interface.IRepositories;
using SchoolManagement.Infrastructure.context;

namespace SchoolManagement.Infrastructure.Repositories
{
	public class StudentRepository : GenericRepository<Student>, IStudentRepository
	{
		private readonly SchoolDbContext _context;
		public StudentRepository(SchoolDbContext context) :base(context)
		{
			_context = context;
		}
	}
}

