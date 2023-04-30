using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Interfaces.Models.Reporting;

[Owned]
public class Location
{
    [StringLength(100)]
    public string? Address { get; set; }

    [StringLength(50)]
    public string? City { get; set; }

    [StringLength(50)]
    public string? State { get; set; }

    [StringLength(10)]
    public string? Zip { get; set; }

    public LocationTypeEnum LocationType { get; set; } = LocationTypeEnum.Default;
}