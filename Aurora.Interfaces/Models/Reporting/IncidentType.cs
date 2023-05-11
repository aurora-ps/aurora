using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace Aurora.Interfaces.Models.Reporting;

public class IncidentType : IIncidentTypeOverrides
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    public string Id { get; set; }

    public virtual ICollection<AgencyIncidentType> AgencyIncidentTypes { get; set; }

    #region Report Options

    public bool CollectTime { get; set; }

    public bool RequiresTime { get; set; }

    public bool CollectLocation { get; set; }

    public bool CollectPerson { get; set; }

    public bool ShowGospelPresentations { get; set; }
    
    public bool ShowProfessionsOfFaith { get; set; }
    
    public bool ShowBaptisms { get; set; }
    
    public bool ShowBibleStudies { get; set; }
    
    public bool ShowCounselingOpportunities { get; set; }

    #endregion

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