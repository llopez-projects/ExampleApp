using core.DTOs;
using core.Interfaces;
using System.Net.Http.Json;

namespace core.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient _http;

        public EmployeeService(HttpClient http)
        {
            _http = http;
        }     

        public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
            var employees = await _http.GetFromJsonAsync<List<EmployeeDto>>("api/employees") ?? [];
            return employees;
        }

        public async Task<EmployeeDto?> GetByIdAsync(int id)
        {
            var employee = await _http.GetFromJsonAsync<EmployeeDto>($"api/employees/{id}");
            return employee;
        }

        public async Task<bool> CreateAsync(CreateEmployeeDto createEmployeeDto)
        {
            var response = await _http.PostAsJsonAsync("api/employees", createEmployeeDto);
            return response.IsSuccessStatusCode;
        }
    }
}
