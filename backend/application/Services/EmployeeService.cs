using application.DTOs;
using application.Helpers;
using application.Helpers.Config;
using application.Helpers.Constants;
using application.Interfaces.Repositories;
using application.Interfaces.Services;
using AutoMapper;
using domain.Entities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;
        private readonly CacheSettings _cacheSettings;
        private readonly IMemoryCache _cache;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository repository, IMapper mapper, IMemoryCache cache, IOptions<CacheSettings> cacheOptions)
        {
            _repository = repository;
            _mapper = mapper;
            _cache = cache;
            _cacheSettings = cacheOptions.Value;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {            
            var expiration = TimeSpan.FromMinutes(_cacheSettings.EmployeesExpirationMinutes);

            var result = await CacheHelper.GetOrCreateAsync(
                _cache,
                CacheKeys.EmployeesAll,
                async () =>
                {
                    var employees = await _repository.GetAllAsync();
                    return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
                },
                expiration);

            return result;
        }

        public async Task<EmployeeDto?> GetByIdAsync(int id)
        {
            var employee = await _repository.GetByIdAsync(id);
            return employee == null ? null : _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<IEnumerable<EmployeeDto>> GetByDepartmentNameAsync(string departmentName)
        {
            var employees = await _repository.GetByDepartmentNameAsync(departmentName);
            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        public async Task<int> CreateAsync(CreateEmployeeDto dto)
        {
            var employee = _mapper.Map<Employee>(dto);
            
            await _repository.AddAsync(employee);

            _cache.Remove(CacheKeys.EmployeesAll);
            
            return employee.Id;
        }
    }
}
