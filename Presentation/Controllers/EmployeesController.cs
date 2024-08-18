using Microsoft.AspNetCore.Mvc;
using Service.Abstractions;
using Shared.DataTransferObjects;

namespace Presentation.Controllers;

[Route("api/companies/{companyId}/employees")]
[ApiController]
public class EmployeesController(IServiceManager service) 
    : ControllerBase
{
    private readonly IServiceManager _service = service;

    [HttpGet]
    public IActionResult GetEmployeesForCompany(Guid companyId)
    {
        IEnumerable<EmployeeDto> employees = _service.EmployeeService.GetEmployees(companyId, trackChanges: false);

        return Ok(employees);
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetEmployeeForCompany(Guid companyId, Guid id)
    {
        EmployeeDto employee = _service.EmployeeService.GetEmployee(companyId, id, trackChanges: false);

        return Ok(employee);
    }

    [HttpPost]
    public IActionResult CreateEmployeeForCompany(Guid companyId, [FromBody] EmployeeForCreationDto employee)
    {
        if (employee is null)
            return BadRequest("EmployeeForCreation object is null.");

        EmployeeDto employeeToReturn = _service.EmployeeService.CreateEmployeeForCompany(companyId, employee, trackChanges: false);

        return CreatedAtRoute("GetEmployeeForCompany", new { companyId, id = employeeToReturn.Id }, employeeToReturn);
    }
}
