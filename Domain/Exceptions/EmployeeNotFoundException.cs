using Domain.Exceptions.Base;

namespace Domain.Exceptions;

public class EmployeeNotFoundException(Guid employeeId) 
    : NotFoundException($"The employee with identifier {employeeId} is not found.")
{
}
