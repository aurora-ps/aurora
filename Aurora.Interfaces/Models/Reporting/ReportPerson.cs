namespace Aurora.Interfaces.Models.Reporting;

public class ReportPerson : Person
{
    public DateTime? DateOfBirth { get; set; }

    public Location Location { get; set; } = new();

    public PhoneNumber? PhoneNumber { get; set; }

    public PersonType Type { get; set; }

    public bool AllowPhoneNumber() => this.Type != PersonType.Victim;

    public override string ToString() => $"{DisplayName} ({Type})";

    private string DisplayName => string.IsNullOrEmpty(FullName) ? "New Person" : FullName;

    public bool AllowAddress() => this.Type != PersonType.Victim;
}