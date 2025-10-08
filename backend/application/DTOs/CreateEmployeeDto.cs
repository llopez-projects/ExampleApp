namespace application.DTOs
{
    public class CreateEmployeeDto
    {
        public string FirstName { get; set; } 
        public string LastName { get; set; }  
        public string Email { get; set; }     
        public DateTime DateHired { get; set; }  
        public int DepartmentID { get; set; }    
    }

}
