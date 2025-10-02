using DataInsights.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DataInsights.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("sales/monthly/{year}/{month}")]
        public async Task<IActionResult> GetMonthlySalesReport(int month, int year)
        {
            var report = await _reportService.GetMonthlySales(month, year);
            return Ok(report);
        }
    }
}
