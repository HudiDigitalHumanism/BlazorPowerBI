using System.Collections.Generic;

namespace BlazorPowerBI.Shared.Reports
{
    public class ReportsConfig
    {
        public string Dataset { get; set; }
        public List<ReportConfig> Reports { get; set; }
    }
    public class ReportConfig
    {
        public string ReportId { get; set; }
        public string ReportName { get; set; }
        public string ReportDisplayName { get; set; }
    }
}
