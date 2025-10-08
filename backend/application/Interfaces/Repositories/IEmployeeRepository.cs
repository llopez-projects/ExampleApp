using domain;

namespace application.Interfaces.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee> GetByIdAsync(int id);
        Task<IEnumerable<Employee>> GetByDepartmentNameAsync(string departmentName);
        Task AddAsync(Employee employee);        
    }
}
