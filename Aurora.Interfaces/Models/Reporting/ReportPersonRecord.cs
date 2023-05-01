namespace Aurora.Interfaces.Models.Reporting;

[GenerateSerializer]
[Immutable]
public record ReportPersonRecord
{
    [Id(0)] public int Id { get; set; }
    [Id(1)] public string? FirstName { get; set; }
    [Id(2)] public string? LastName { get; set; }
    [Id(3)] public string? MiddleName { get; set; }
    [Id(4)] public string? Suffix { get; set; }
    [Id(5)] public DateTime? DateOfBirth { get; set; }
    [Id(6)] public LocationRecord Location { get; set; } = new();
    [Id(7)] public PhoneNumberRecord? PhoneNumber { get; set; }
    [Id(8)] public PersonType Type { get; set; }
    

    public override string ToString() => $"{DisplayName} ({Type})";

    private string DisplayName => string.IsNullOrEmpty(FullName) ? "New Person" : FullName;

    public string FullName => $"{FirstName} {LastName}".Trim();
}