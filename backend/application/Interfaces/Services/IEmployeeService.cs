using application.DTOs;

namespace application.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllAsync();
        Task<EmployeeDto?> GetByIdAsync(int id);
        Task<IEnumerable<EmployeeDto>> GetByDepartmentNameAsync(string departmentName);
        Task<int> CreateAsync(CreateEmployeeDto dto);
    }
}
