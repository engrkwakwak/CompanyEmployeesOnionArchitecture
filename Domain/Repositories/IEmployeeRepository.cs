﻿using Domain.Entities;

namespace Domain.Repositories;

public interface IEmployeeRepository
{
    IEnumerable<Employee> GetEmployees(Guid companyId, bool trackChanges);

    Employee? GetEmployee(Guid companyId, Guid id, bool trackChanges);

    void CreateEmployeeForCompany(Guid companyId, Employee employee);
}
