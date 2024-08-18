using Domain.Exceptions.Base;

namespace Domain.Exceptions;

public sealed class CompanyNotFoundException(Guid companyId) 
    : NotFoundException($"The company with identifier {companyId} is not found.")
{
}
