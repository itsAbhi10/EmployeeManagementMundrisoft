using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Models
{
    public class Databasecontext : DbContext
    {
       public Databasecontext() { }
        public Databasecontext(DbContextOptions<Databasecontext> options) : base(options) { }

        public virtual DbSet<RegistrationModel> RegistrationModel { get; set; }

        public virtual DbSet<EmployeeModel> Employee { get; set; }
    }
}
