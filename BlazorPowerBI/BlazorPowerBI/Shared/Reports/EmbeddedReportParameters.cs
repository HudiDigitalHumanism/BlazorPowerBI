using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorPowerBI.Shared.Reports
{
    public class EmbeddedReportParameters
    {
        public Dataset[] Datasets { get; set; } = Array.Empty<Dataset>();
        public Report[] Reports { get; set; } = Array.Empty<Report>();
        public string Username { get; set; }
    }

    public class Dataset
    {
        public string Id { get; set; }
    }

    public class Report
    {
        public bool AllowEdit { get; set; }
        public string Id { get; set; }
    }
}
