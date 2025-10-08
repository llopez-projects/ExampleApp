namespace core.DTOs
{
    public class CreateEmployeeDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateHired { get; set; } = DateTime.Now.AddDays(-1);
        public int DepartmentId { get; set; }
    }
}
