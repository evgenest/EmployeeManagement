using EmployeeManagement.Data;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EmployeeManagement.Repositories;

public class EmployeeRepository(AppDbContext context) : IEmployeeRepository
{
  private readonly AppDbContext _context = context;

  public async Task AddEmployeeAsync(Employee employee)
  {
    await _context.Employees.AddAsync(employee);
    await _context.SaveChangesAsync();
  }
  public async Task DeleteEmployeeAsync(int id)
  {
    _context.Employees.Remove(await GetByIdAsync(id) ?? throw new KeyNotFoundException($"Employee with id {id} was not found."));
    await _context.SaveChangesAsync();
  }

  public async Task<IEnumerable<Employee>> GetAllAsync()
  {
    return await _context.Employees.ToListAsync();
  }

  public async Task<Employee?> GetByIdAsync(int id)
  {
    return await _context.Employees.FindAsync(id);
  }

  public async Task UpdateEmployeeAsync(Employee employee)
  {
    _context.Employees.Update(employee);
    await _context.SaveChangesAsync();
  }
}
