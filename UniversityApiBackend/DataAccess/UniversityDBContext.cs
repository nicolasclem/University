using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.DataAccess
{
    public class UniversityDBContext : DbContext
    {
        public UniversityDBContext(DbContextOptions<UniversityDBContext> options): base(options)
        {

        }

        //TODO :  Add DbSets ( tables of our Data Base)
        public DbSet<User>? Users { get; set; }

        public DbSet<Curso>? Cursos { get; set; }
    }

}
