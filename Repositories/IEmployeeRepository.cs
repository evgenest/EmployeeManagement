using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EmployeeManagement.Repositories;

public interface IEmployeeRepository
{

  Task<IEnumerable<Employee>> GetAllAsync();
  Task<Employee?> GetByIdAsync(int id);
  Task AddEmployeeAsync(Employee employee);
  Task UpdateEmployeeAsync(Employee employee);
  Task DeleteEmployeeAsync(int id);
}
