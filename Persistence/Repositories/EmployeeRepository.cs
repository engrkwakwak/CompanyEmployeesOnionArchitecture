using Domain.Entities;
using Domain.Repositories;

namespace Persistence.Repositories;

public class EmployeeRepository(RepositoryContext repositoryContext) 
    : RepositoryBase<Employee>(repositoryContext), IEmployeeRepository
{
}
