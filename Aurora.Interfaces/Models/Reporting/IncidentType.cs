using System.Security.Cryptography;
using System.Text;

namespace Aurora.Interfaces.Models.Reporting;

public class IncidentType
{
    public string Name { get; set; }

    public string Id { get; set; }

    public bool CollectTime { get; set; } = false;

    public bool RequiresTime { get; set; } = false;

    public bool CollectLocation { get; set; } = false;

    public bool CollectPerson { get; set; } = false;
    
    public override int GetHashCode()
    {
        using MD5 md5 = MD5.Create();
        var hash = md5.ComputeHash(Encoding.Default.GetBytes(Id ?? ""));
        return BitConverter.ToInt32(hash, 0);
    }

    public override bool Equals(object obj)
    {
        return obj is IncidentType incidentType && incidentType.Id == Id;
    }

    public override string ToString()
    {
        return Name;
    }
}