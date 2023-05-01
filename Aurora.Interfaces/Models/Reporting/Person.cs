using System.ComponentModel.DataAnnotations;

namespace Aurora.Interfaces.Models.Reporting;

public class Person
{
    public int Id { get; set; }

    [StringLength(50)]
    public string FirstName { get; set; }

    [StringLength(50)]
    public string LastName { get; set; }

    public string FullName => $"{FirstName} {LastName}".Trim();

    public override string ToString() => FullName;
}