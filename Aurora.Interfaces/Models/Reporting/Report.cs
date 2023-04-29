using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Interfaces.Models.Reporting
{
    public class Report
    {
        public Report()
        {
            Location = new Location { LocationType = LocationType.Incident };
        }

        public DateTime? Date { get; set; } = DateTime.Now;
        public TimeSpan? Time { get; set; }
        public Agency Agency { get; set; }
        public IncidentType IncidentType { get; set; }
        public double? Miles { get; set; }
        public Location Location { get; set; }

        public string Narrative { get; set; }

        public IList<ReportPerson> People { get; set; } = new List<ReportPerson>();
    }
}
