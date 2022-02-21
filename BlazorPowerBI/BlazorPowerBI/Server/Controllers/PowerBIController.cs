using System.Linq;
using System.Threading.Tasks;
using BlazorPowerBI.Shared.Reports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using BlazorPowerBI.Shared.ActiveDirectory;

namespace BlazorPowerBI.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PowerBIController : ControllerBase
    {
        private readonly AuthApplicationOptions applicationOptions;
        private readonly IOptions<ReportsConfig> reportOptions;

        public PowerBIController(IOptions<AuthApplicationOptions> applicationOptions, IOptions<ReportsConfig> reportOptions)
        {
            this.applicationOptions = applicationOptions.Value;
            this.reportOptions = reportOptions;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var app = ConfidentialClientApplicationBuilder.Create(applicationOptions.ClientId)
                .WithTenantId(applicationOptions.TenantId)
                .WithClientSecret(applicationOptions.ClientSecret)
                .Build();

            var result = await app.AcquireTokenForClient(applicationOptions.Scopes)
                .ExecuteAsync();

            return Ok(new AuthApplicationResult
            {
                AccessToken = result.AccessToken,
                ExpiresOn = result.ExpiresOn,
                IdToken = result.IdToken
            });
        }

        [HttpGet("config/{reportName}")]
        public IActionResult GetReportConfig(string reportName)
        {
            var report = reportOptions.Value.Reports.FirstOrDefault(t => t.ReportName == reportName);
            if (report == null)
                return NotFound();

            return Ok(new ReportConfiguration
            {
                Dataset = reportOptions.Value.Dataset,
                ReportId = report.ReportId,
                ReportDisplayName = report.ReportDisplayName,
                ReportName = report.ReportName
            });
        }

        [HttpGet("configs")]
        public IActionResult GetAllReportConfig()
        {
            var model = new ReportsConfig
            {
                Dataset = reportOptions.Value.Dataset,
                Reports = reportOptions.Value.Reports
            };
            return Ok(model);
        }
    }
}
