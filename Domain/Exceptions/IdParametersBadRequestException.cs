using Domain.Exceptions.Base;

namespace Domain.Exceptions;

public sealed class IdParametersBadRequestException()
    : BadRequestException("Parameter ids is null.")
{
}