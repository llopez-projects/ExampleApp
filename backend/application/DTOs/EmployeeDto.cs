namespace application.DTOs
{
    public class EmployeeDto
    {
        public int Id { get; set; } 
        public string FullName { get; set; }
        public string Email { get; set; }   
        public DateTime DateHired { get; set; }  
        public string DepartmentName { get; set; }
    }

}
