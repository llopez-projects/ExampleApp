using application.Helpers.Config;
using application.Interfaces.Repositories;
using application.Mapping;
using application.Services;
using AutoMapper;
using domain;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Moq;

namespace application.Tests
{
    public class EmployeeServiceTests
    {
        private readonly Mock<IEmployeeRepository> _repoMock = new();
        private readonly IMapper _mapper;

        public EmployeeServiceTests()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsMappedDto_WhenEmployeeExists()
        {
            var employee = GetEmployees().First();
            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(employee);

            var cacheMock = new Mock<IMemoryCache>();
            var cacheOptions = Options.Create(new CacheSettings
            {
                DefaultExpirationMinutes = 5,
                EmployeesExpirationMinutes = 3
            });

            var service = new EmployeeService(_repoMock.Object, _mapper, cacheMock.Object, cacheOptions);

            var result = await service.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal("Ana G�mez", result.FullName);
            Assert.Equal("Tecnolog�a", result.DepartmentName);
        }

        [Fact]
        public async Task GetByDepartmentNameAsync_ReturnsMappedDtos_WhenEmployeesExist()
        {
            // Arrange
            var departmentName = "Tecnolog�a";
            var employees = GetEmployees();

            _repoMock.Setup(r => r.GetByDepartmentNameAsync(departmentName))
                     .ReturnsAsync(employees);

            var cacheMock = new Mock<IMemoryCache>();
            var cacheOptions = Options.Create(new CacheSettings
            {
                DefaultExpirationMinutes = 5,
                EmployeesExpirationMinutes = 3
            });

            var service = new EmployeeService(_repoMock.Object, _mapper, cacheMock.Object, cacheOptions);

            // Act
            var result = await service.GetByDepartmentNameAsync(departmentName);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);    
            result.First().FullName.Should().Be("Ana G�mez");
        }

        private List<Employee> GetEmployees()
        {
            return
            [
                new Employee
                {
                    Id = 1,
                    FirstName = "Ana",
                    LastName = "G�mez",
                    Email = "ana@empresa.com",
                    DateHired = new DateTime(2022, 1, 10),
                    Department = new Department { Name = "Tecnolog�a" }
                },
                new Employee
                {
                    Id = 2,
                    FirstName = "Luis",
                    LastName = "G�mez",
                    Email = "luis@empresa.com",
                    DateHired = new DateTime(2023, 5, 20),
                    Department = new Department { Name = "Tecnolog�a" }
                }
            ];
        }
    }
}

