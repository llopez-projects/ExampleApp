using domain.Entities;
using infrastructure.Persistence;
using infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;


namespace infrastructure.tests
{
    public class EmployeeRepositoryTests
    {
        private readonly DatabaseContext _context;

        public EmployeeRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            _context = new DatabaseContext(options);
            _context.Departments.Add(new Department { Id = 1, Name = "Tecnología" });
            _context.SaveChanges();
        }

        [Fact]
        public async Task AddAsync_PersistsEmployee()
        {
            var repo = new EmployeeRepository(_context);
            var employee = new Employee { FirstName = "Ana", LastName = "Gómez", Email = "ana.gomez@empresa.com", DepartmentId = 1 };

            await repo.AddAsync(employee);
            var result = await repo.GetByIdAsync(employee.Id);

            Assert.NotNull(result);
            Assert.Equal("Ana", result.FirstName);
        }
    }

}