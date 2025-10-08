using core.DTOs;

namespace core.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllAsync();
        Task<EmployeeDto?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateEmployeeDto dto);
    }
}
