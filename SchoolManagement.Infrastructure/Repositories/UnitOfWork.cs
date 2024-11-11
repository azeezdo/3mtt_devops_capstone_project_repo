using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections;
using SchoolManagement.Domain.Interface;
using SchoolManagement.Infrastructure.context;
using SchoolManagement.Domain.Interface.IRepositories;

namespace SchoolManagement.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SchoolDbContext _dbContext;
        private Hashtable _repositories;
        public DatabaseFacade Database => _dbContext.Database;
        public IStudentRepository studentRepo {get; private set;}
        public UnitOfWork(SchoolDbContext dbContext)
		{
            _dbContext = dbContext;
            studentRepo = new StudentRepository(dbContext);
		}
        public IGenericRepository<TEntity> repository<TEntity>() where TEntity : class
        {
            if (_repositories == null) _repositories = new Hashtable();
            var Type = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(Type))
            {
                var repositoryType = typeof(GenericRepository<TEntity>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _dbContext);
                _repositories.Add(Type, repositoryInstance);
            }
            return (IGenericRepository<TEntity>)_repositories[Type];
        }

        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

       
    }
}

