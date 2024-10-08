﻿using Shared.DataTransferObjects;

namespace Service.Abstractions;

public interface ICompanyService
{
    IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges);
    IEnumerable<CompanyDto> GetByIds(IEnumerable<Guid> ids, bool trackChanges);

    CompanyDto GetCompany(Guid id, bool trackChanges);

    CompanyDto CreateCompany(CompanyForCreationDto company);

    (IEnumerable<CompanyDto> companies, string ids) CreateCompanyCollection(IEnumerable<CompanyForCreationDto> companyCollection);
}
