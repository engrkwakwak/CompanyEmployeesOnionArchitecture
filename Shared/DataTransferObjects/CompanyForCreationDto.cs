namespace Shared.DataTransferObjects;

//public record CompanyForCreationDto
//{
//    public string? Name { get; init; }
//    public string? Address { get; init; }
//    public string? Country { get; init; }

//    IEnumerable<EmployeeForCreationDto>? Employees { get; init; }
//}

public record CompanyForCreationDto(string Name, string Address, string Country, IEnumerable<EmployeeForCreationDto> Employees);
