namespace Aurora.Interfaces.Models.Reporting;

[GenerateSerializer]
[Immutable]
public record ReportPersonRecord
{
    [Id(0)] public string FullName { get; init; } = string.Empty;
    [Id(1)] public string? FirstName { get; init; }
    [Id(2)] public string? LastName { get; init; }
    [Id(3)] public string? MiddleName { get; init; }
    [Id(4)] public string? Suffix { get; init; }
    [Id(5)] public DateTime? DateOfBirth { get; init; }
    [Id(6)] public LocationRecord Location { get; init; } = new();
    [Id(7)] public PhoneNumberRecord? PhoneNumber { get; init; }
    [Id(8)] public PersonType Type { get; init; }
}