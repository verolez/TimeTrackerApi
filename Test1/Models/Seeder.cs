using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Test1.Models
{
    public static class Seeder 
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new TimeManagementContext(
                serviceProvider.GetRequiredService<DbContextOptions<TimeManagementContext>>()))
            {
                if (context.Employees.Any())
                {
                    return;
                }

                IList<Employee> employees = new List<Employee>
                {
                    //new Employee() { ID = 1, Name = "Sample 1", ClockInTime = DateTime.UtcNow, ClockOutTime = DateTime.UtcNow, isActive = true },
                    //new Employee() { ID = 2, Name = "Sample 2", ClockInTime = DateTime.UtcNow, ClockOutTime = DateTime.UtcNow, isActive = true }
                };

                context.AddRange(employees);

                context.SaveChanges();
            }
        }
    }
}
