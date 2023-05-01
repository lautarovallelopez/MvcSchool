using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcSchool.Models;

namespace MvcSchool.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext (DbContextOptions<SchoolContext> options)
            : base(options)
        {
        }

        public DbSet<MvcSchool.Models.School> School { get; set; } = default!;
        public DbSet<MvcSchool.Models.Principal> Principal { get; set; } = default!;
        public DbSet<MvcSchool.Models.Teacher> Teacher { get; set; } = default!;
        public DbSet<MvcSchool.Models.Student> Student { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<School>()
                .HasOne(school => school.Principal)
                .WithOne(principal => principal.School)
                .HasForeignKey<Principal>(principal => principal.SchoolId)
                .IsRequired(false);
            modelBuilder.Entity<School>()
                .HasMany(school => school.Teachers)
                .WithOne(teacher => teacher.School)
                .HasForeignKey(teacher => teacher.SchoolId)
                .IsRequired(false);

            modelBuilder.Entity<Teacher>()
                .HasMany(teacher => teacher.Students)
                .WithMany(student => student.Teachers);
        }
    }
}
