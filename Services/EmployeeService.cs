using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Service.Abstractions;
using Shared.DataTransferObjects;

namespace Services;

internal sealed class EmployeeService(
    IRepositoryManager repository, 
    ILoggerManager logger,
    IMapper mapper
) : IEmployeeService
{
    private readonly IRepositoryManager _repository = repository;
    private readonly ILoggerManager _logger = logger;
    private readonly IMapper _mapper = mapper;
    

    public IEnumerable<EmployeeDto> GetEmployees(Guid companyId, bool trackChanges)
    {
        _ = _repository.Company.GetCompany(companyId, trackChanges)
            ?? throw new CompanyNotFoundException(companyId);

        IEnumerable<Employee> employeesFromDb = _repository.Employee.GetEmployees(companyId, trackChanges);
        IEnumerable<EmployeeDto> employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeesFromDb);

        return employeesDto;
    }

    public EmployeeDto GetEmployee(Guid companyId, Guid id, bool trackChanges)
    {
        _ = _repository.Company.GetCompany(companyId, trackChanges)
            ?? throw new CompanyNotFoundException(companyId);

        Employee employee = _repository.Employee.GetEmployee(companyId, id, trackChanges)
            ?? throw new EmployeeNotFoundException(id);

        EmployeeDto employeeDto = _mapper.Map<EmployeeDto>(employee);
        return employeeDto;
    }

    public EmployeeDto CreateEmployeeForCompany(Guid companyId, EmployeeForCreationDto employeeForCreation, bool trackChanges)
    {
        _ = _repository.Company.GetCompany(companyId, trackChanges)
            ?? throw new CompanyNotFoundException(companyId);

        Employee employeeEntity = _mapper.Map<Employee>(employeeForCreation);

        _repository.Employee.CreateEmployeeForCompany(companyId, employeeEntity);
        _repository.Save();

        EmployeeDto employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);
        return employeeToReturn;
    }
}
