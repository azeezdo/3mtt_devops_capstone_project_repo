using System;
using SchoolManagement.Domain.Interface.IRepositories;

namespace SchoolManagement.Domain.Interface
{
	public interface IUnitOfWork
	{
        IGenericRepository<TEntity> repository<TEntity>() where TEntity : class;
        Task<int> CompleteAsync();
        IStudentRepository studentRepo { get; }
    }
}

