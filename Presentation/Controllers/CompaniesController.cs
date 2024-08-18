﻿using Microsoft.AspNetCore.Mvc;
using Presentation.ModelBinders;
using Service.Abstractions;
using Shared.DataTransferObjects;

namespace Presentation.Controllers;

[Route("api/companies")]
[ApiController]
public class CompaniesController(IServiceManager service) : ControllerBase
{
    private readonly IServiceManager _service = service;

    [HttpGet]
    public IActionResult GetCompanies()
    {
        IEnumerable<CompanyDto> companies = _service.CompanyService.GetAllCompanies(trackChanges: false);
        return Ok(companies);
    }

    [HttpGet("collection/({ids})", Name = "CompanyCollection")]
    public IActionResult GetCompanyCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
    {
        IEnumerable<CompanyDto> companies = _service.CompanyService.GetByIds(ids, trackChanges: false);

        return Ok(companies);
    }

    [HttpGet("{id:guid}", Name = "CompanyById")]
    public IActionResult GetCompany(Guid id)
    {
        var company = _service.CompanyService.GetCompany(id, trackChanges: false);

        return Ok(company);
    }

    [HttpPost]
    public IActionResult CreateCompany([FromBody] CompanyForCreationDto company)
    {
        if (company is null)
            return BadRequest("CompanyForCreationDto object is null.");

        CompanyDto createdCompany = _service.CompanyService.CreateCompany(company);

        return CreatedAtRoute("CompanyById", new { id = createdCompany.Id }, createdCompany);
    }

    [HttpPost("collection")]
    public IActionResult CreateCompanyCollection([FromBody] IEnumerable<CompanyForCreationDto> companyCollection)
    {
        var (companies, ids) = _service.CompanyService.CreateCompanyCollection(companyCollection);

        return CreatedAtRoute("CompanyCollection", new {ids}, companies);
    }
}
