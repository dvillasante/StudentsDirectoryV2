using Microsoft.EntityFrameworkCore;
using Models;
using System;

namespace Context
{
    public class StudentContext : DbContext 
    {
        public StudentContext(DbContextOptions<StudentContext> DbContextOptions) : base(DbContextOptions){ }

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasKey(x => x.Id);
           
        }
    }
}
