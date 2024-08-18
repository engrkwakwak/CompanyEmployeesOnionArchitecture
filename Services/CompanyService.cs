using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Service.Abstractions;
using Shared.DataTransferObjects;

namespace Services;

internal sealed class CompanyService(
    IRepositoryManager repository, 
    ILoggerManager logger,
    IMapper mapper
)   : ICompanyService
{
    private readonly IRepositoryManager _repository = repository;
    private readonly ILoggerManager _logger = logger;
    private readonly IMapper _mapper = mapper;

    public IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges)
    {
        IEnumerable<Company> companies = _repository.Company.GetAllCompanies(trackChanges);
        IEnumerable<CompanyDto> companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
        return companiesDto;
    }

    public IEnumerable<CompanyDto> GetByIds(IEnumerable<Guid> ids, bool trackChanges)
    {
        if (ids is null)
            throw new IdParametersBadRequestException();

        IEnumerable<Company> companyEntities = _repository.Company.GetByIds(ids, trackChanges);
        if (ids.Count() != companyEntities.Count())
            throw new CollectionByIdsBadRequestException();

        IEnumerable<CompanyDto> companiesToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);
        return companiesToReturn;
    }

    public CompanyDto GetCompany(Guid id, bool trackChanges)
    {
        Company company = _repository.Company.GetCompany(id, trackChanges)
            ?? throw new CompanyNotFoundException(id);

        CompanyDto companyDto = _mapper.Map<CompanyDto>(company);
        return companyDto;
    }

    public CompanyDto CreateCompany(CompanyForCreationDto company)
    {
        Company companyEntity = _mapper.Map<Company>(company);

        _repository.Company.CreateCompany(companyEntity);
        _repository.Save();

        CompanyDto companyToReturn = _mapper.Map<CompanyDto>(companyEntity);

        return companyToReturn;
    }

    public (IEnumerable<CompanyDto> companies, string ids) CreateCompanyCollection(IEnumerable<CompanyForCreationDto> companyCollection)
    {
        if (companyCollection is null)
            throw new CompanyCollectionBadRequest();

        IEnumerable<Company> companyEntities = _mapper.Map<IEnumerable<Company>>(companyCollection);
        foreach(Company company in companyEntities)
        {
            _repository.Company.CreateCompany(company);
        }

        _repository.Save();

        IEnumerable<CompanyDto> companyCollectionToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);
        var ids = string.Join(",", companyCollectionToReturn.Select(x => x.Id));

        return (companies: companyCollectionToReturn, ids: ids);
    }
}
