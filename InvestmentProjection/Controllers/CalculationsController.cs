using InvestmentProjection.BusinessLogic.Handlers.CalculationsHandler;
using InvestmentProjection.BusinessLogic.Models;
using InvestmentProjection.BusinessLogic.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace InvestmentProjection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CalculationsController : ControllerBase
    {
        [HttpGet("[action]")]
        public JsonResult CalculateChartData(
            [Required] decimal lumpSumInvestment,
            [Required] decimal monthlyInvestment,
            [Required] decimal targetValue,
            [Required] int timeScale,
            [Required] RiskLevel riskLevel)
        {
            var request = new CalculateChartDataRequest()
            {
                LumpSumInvestment = lumpSumInvestment,
                MonthlyInvestment = monthlyInvestment,
                TargetValue = targetValue,
                Timescale = timeScale,
                RiskLevel = riskLevel
            };

            var chartData = new CalculationsHandler().GetChartData(request);

            return new JsonResult(chartData);
        }
    }
}
