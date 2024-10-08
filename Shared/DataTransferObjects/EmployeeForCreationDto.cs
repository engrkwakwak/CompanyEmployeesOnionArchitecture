﻿namespace Shared.DataTransferObjects;

public record EmployeeForCreationDto
{
    public string? Name { get; init; }
    public int Age { get; init; }
    public string? Position { get; init; }
}