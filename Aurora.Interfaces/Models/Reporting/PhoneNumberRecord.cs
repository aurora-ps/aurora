namespace Aurora.Interfaces.Models.Reporting;

[GenerateSerializer]
[Immutable]
public sealed record PhoneNumberRecord
{
    [Id(0)] public string Number { get; init; } = string.Empty;
    [Id(1)] public PhoneNumberTypeEnum Type { get; init; }
}