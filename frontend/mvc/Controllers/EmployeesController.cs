using core.DTOs;
using core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace mvc.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task<IActionResult> Index()
        {
            var employees = await _employeeService.GetAllAsync();
            return View(employees);
        }

        public async Task<IActionResult> Details(int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee == null) return NotFound();
            return View(employee);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Departments = new SelectList(GetDepartments(), "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeDto employee)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Datos inválidos. Revisá el formulario.";
                return View(employee);
            }

            var success = await _employeeService.CreateAsync(employee);
            if (success) 
            {
                TempData["SuccessMessage"] = "Empleado creado correctamente.";
                return RedirectToAction("Index");
            }

            ViewBag.Departments = new SelectList(GetDepartments(), "Id", "Name");
            TempData["ErrorMessage"] = "No se pudo crear el empleado.";
            return View(employee);
        }

        private List<DepartmentDto> GetDepartments() 
        {
            return [
                new DepartmentDto { Id = 1, Name = "Tecnología" },
                new DepartmentDto { Id = 2, Name = "Recursos Humanos" },
                new DepartmentDto { Id = 3, Name = "Finanzas" }
            ];
        }
    }
}
