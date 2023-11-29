namespace Dot_Net_Task.Models
{
    public class Employee
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public int? ManagerID { get; set; }
        public decimal EmployeeSalary { get; set; }
    }
}
