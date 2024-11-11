using System;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SchoolManagement.Domain.Entity;

namespace SchoolManagement.Infrastructure.context
{
	public class SchoolDbContext : DbContext
	{
        protected readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options, IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("SchoolManagementDB"));
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
    }
}

