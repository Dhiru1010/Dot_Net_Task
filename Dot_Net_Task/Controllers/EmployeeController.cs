using Dot_Net_Task.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dot_Net_Task.Controllers
{
	public class EmployeeController : Controller
	{
		public List<Employee> Employees { get; set; } = new List<Employee>() {
			new Employee { ID = 1, Name = "A", ManagerID = null, EmployeeSalary = 10000 },
			new Employee { ID = 2, Name = "B", ManagerID = 1, EmployeeSalary = 4000 },
			new Employee { ID = 3, Name = "C", ManagerID = 2, EmployeeSalary = 3000 },
			new Employee { ID = 4, Name = "D", ManagerID = 1, EmployeeSalary = 3500 },
			new Employee { ID = 5, Name = "E", ManagerID = 3, EmployeeSalary = 2500 },
			new Employee { ID = 6, Name = "F", ManagerID = 1, EmployeeSalary = 1500 }
		};
		public IActionResult Index()
		{
			List<Employee> employees = Employees;
			return View(employees);
		}

		// Action method for Query 1
		public IActionResult EmployeeManagerName()
		{
			var result = Employees.Where(x => x.ManagerID != null)
				.Select(e => new { EmployeeName = e.Name, ManagerName = Employees.FirstOrDefault(m => m.ID == e.ManagerID)!.Name })
				.ToList(); ;
			return Json(result);
		}

		// Action method for Query 2
		public IActionResult ManageNameAndEmployee()
		{
			var result = Employees.Where(x => x.ManagerID != null)
				.GroupBy(e => new { ManagerName = Employees.FirstOrDefault(m => m.ID == e.ManagerID)!.Name })
				.Select(g => new { g.Key.ManagerName, NumberOfEmployees = g.Count() })
				.ToList();
			return Json(result);
		}

		// Action method for Query 3
		public IActionResult ManagerNameSalary()
		{
			var result = Employees
				.Where(e => e.ManagerID.HasValue)
				.Select(e =>
				{
					return new { ManagerName = Employees.FirstOrDefault(m => m.ID == e.ManagerID)!.Name, ManagerSalary = Employees.FirstOrDefault(m => m.ID == e.ManagerID)!.EmployeeSalary };
				}).DistinctBy(x=>x.ManagerName)
				.ToList();
			return Json(result);
		}

		// Action method for Query 4
		public IActionResult SecondHighestSalaryEmployee()
		{
			var result = Employees
			.OrderByDescending(e => e.EmployeeSalary)
			.Skip(1)
			.Select(e => new { ManagerName = Employees.FirstOrDefault(m => m.ID == e.ManagerID)!.Name, EmployeeSalary = e.EmployeeSalary })
			.FirstOrDefault();
			return Json(result);
		}
	}
}
