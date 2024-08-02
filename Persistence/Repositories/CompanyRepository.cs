using Domain.Entities;
using Domain.Repositories;

namespace Persistence.Repositories;

public class CompanyRepository(RepositoryContext repositoryContext) 
    : RepositoryBase<Company>(repositoryContext), ICompanyRepository
{
}
