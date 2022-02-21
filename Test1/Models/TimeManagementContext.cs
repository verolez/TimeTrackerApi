using Microsoft.EntityFrameworkCore;
namespace Test1.Models
{
    public class TimeManagementContext : DbContext
    {
        public TimeManagementContext(DbContextOptions<TimeManagementContext> options): base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
