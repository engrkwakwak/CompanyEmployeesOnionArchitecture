using Domain.Exceptions.Base;

namespace Domain.Exceptions;

public sealed class CompanyCollectionBadRequest()
    : BadRequestException("Company collection sent from a client is null.")
{
}
