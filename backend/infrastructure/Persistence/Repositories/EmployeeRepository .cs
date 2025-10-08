using application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using infrastructure.Persistence;
using domain.Entities;

namespace infrastructure.Persistence.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DatabaseContext _context;

        public EmployeeRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees
                .Include(e => e.Department)
                .ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Employee>> GetByDepartmentNameAsync(string departmentName)
        {
            return await _context.Employees
                .Include(e => e.Department)
                .Where(e => e.Department.Name == departmentName)
                .ToListAsync();
        }


        public async Task AddAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
        }
    }
}
