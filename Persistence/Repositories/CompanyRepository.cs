using Domain.Entities;
using Domain.Repositories;

namespace Persistence.Repositories;

internal sealed class CompanyRepository(RepositoryContext repositoryContext) 
    : RepositoryBase<Company>(repositoryContext), ICompanyRepository
{
    public IEnumerable<Company> GetAllCompanies(bool trackChanges) =>
        [.. FindAll(trackChanges).OrderBy(c => c.Name)];

    public IEnumerable<Company> GetByIds(IEnumerable<Guid> ids, bool trackChanges) =>
        [.. FindByCondition(x => ids.Contains(x.Id), trackChanges)];

    public Company? GetCompany(Guid companyId, bool trackChanges) =>
        FindByCondition(c => c.Id.Equals(companyId), trackChanges)
        .SingleOrDefault();

    public void CreateCompany(Company company) => Create(company);
}
