using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test1.Models;
using Microsoft.EntityFrameworkCore;

namespace Test1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly TimeManagementContext _context;

        public EmployeesController(TimeManagementContext context) => _context = context;

        public async Task<IActionResult> Index()
        {
            return Ok(await _context.Employees.ToListAsync());
        }

        [HttpGet("{employeename,isActive}")]
        [Route("api/[controller]/filter")]
        public async Task<IActionResult> FilterEmployees(string EmployeeName, bool isActive)
        {
            if (string.IsNullOrWhiteSpace(EmployeeName))
            {
                return BadRequest();
            }

            return Ok(await _context.Employees.Where(c => c.Name.ToLower() == EmployeeName.ToLower()).ToListAsync());
        }

        [HttpPost]
        [Route("insert")]
        public async Task<IActionResult> Insert([Bind("Name,ClockInTime,ClockOutTime,isActive")] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return Content("Error");
            }
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return Content("Saved Successfully.");
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> Update(Employee Employee)
        {
            try
            {
                _context.Update(Employee);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(Employee.ID))
                    return NotFound();
                else
                    throw;
            }

            return Content("Saved Successfully");
        }

        [HttpPost]
        [Route("delete")]
        public async Task<ContentResult> Delete([FromBody]int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return Content("Successfully Deleted");
        }


        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(c => c.ID == id);
        }

    }
}
