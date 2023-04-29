namespace Aurora.Interfaces.Models.Reporting;

public class Location
{
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zip { get; set; }
    public LocationType LocationType { get; set; } = LocationType.Default;
}