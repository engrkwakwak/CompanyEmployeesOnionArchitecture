using AutoMapper;
using Domain.Entities;
using Shared.DataTransferObjects;

namespace CompanyEmployeesOnionArchitecture;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Company, CompanyDto>()
            .ForMember("FullAddress",
                opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));
        CreateMap<CompanyForCreationDto, Company>();

        CreateMap<Employee, EmployeeDto>();
        CreateMap<EmployeeForCreationDto, Employee>();
    }
        
}