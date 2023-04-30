using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace Aurora.Interfaces.Models.Reporting;

public class Agency
{
    public Agency(string id, string name)
    {
        Id = id;
        Name = name;
    }

    [StringLength(100)]
    public string Name { get; set; }

    [StringLength(50)]
    public string Id { get; set; }

    public override int GetHashCode()
    {
        using MD5 md5 = MD5.Create();
        var hash = md5.ComputeHash(Encoding.Default.GetBytes(Id ?? ""));
        return BitConverter.ToInt32(hash, 0);
    }

    public override bool Equals(object obj)
    {
        return obj is Agency agency && agency.Id == Id;
    }

    public override string ToString()
    {
        return Name;
    }

    public ICollection<IncidentType> IncidentTypes { get; set; } = new List<IncidentType>();
}