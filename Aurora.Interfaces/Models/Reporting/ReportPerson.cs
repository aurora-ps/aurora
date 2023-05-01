namespace Aurora.Interfaces.Models.Reporting;

public class ReportPerson : Person
{
    public DateTime? DateOfBirth { get; set; }

    public Location Location { get; set; } = new();

    public PhoneNumber? PhoneNumber { get; set; }

    public PersonType Type { get; set; }

    public override string ToString() => $"{DisplayName} ({Type})";

    private string DisplayName => string.IsNullOrEmpty(FullName) ? "New Person" : FullName;
}