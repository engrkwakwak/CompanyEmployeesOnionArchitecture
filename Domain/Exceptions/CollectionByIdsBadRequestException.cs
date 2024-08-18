using Domain.Exceptions.Base;

namespace Domain.Exceptions;

public sealed class CollectionByIdsBadRequestException() 
    : BadRequestException("Collection count mismatch comparing ids.")
{
}