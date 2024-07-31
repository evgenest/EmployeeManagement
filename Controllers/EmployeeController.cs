﻿using EmployeeManagement.Models;
using EmployeeManagement.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers;
[Route("api/[controller]")]
[ApiController]
public class EmployeeController(IEmployeeRepository employeeRepository) : ControllerBase
{
  private readonly IEmployeeRepository _employeeRepository = employeeRepository;

  [HttpPost]
  public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
  {
    if (ModelState.IsValid == false) return BadRequest();

    await _employeeRepository.AddEmployeeAsync(employee);
    return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
  {
    return Ok(await _employeeRepository.GetAllAsync());
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<Employee>> GetEmployeeById(int id)
  {
    var employee = await _employeeRepository.GetByIdAsync(id);
    if (employee == null)
    {
      return NotFound($"Employee with id {id} was not found.");
    }
    return Ok(employee);
  }

  [HttpDelete("{id}")]
  public async Task<ActionResult> DeleteEmployeeById(int id)
  {
    await _employeeRepository.DeleteEmployeeAsync(id);
    return NoContent();
  }

  [HttpPut("{id}")]
  public async Task<ActionResult<Employee>> UpdateEmployeeAsync(int id, Employee employee)
  {
    if (id != employee.Id) return BadRequest("Id mismatch.");

    if (ModelState.IsValid == false) return BadRequest();

    await _employeeRepository.UpdateEmployeeAsync(employee);
    return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
  }
}