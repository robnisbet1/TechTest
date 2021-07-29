using InvestmentProjection.BusinessLogic.Models;
using System.ComponentModel.DataAnnotations;

namespace InvestmentProjection.Models
{
    public class InvestmentChartModel
    {
        [Display(Name = "Lump Sum Investment (£)")]
        public decimal LumpSumInvestment { get; init; }

        [Display(Name = "Monthly Investment (£)")]
        public decimal MonthlyInvestment { get; init; }

        [Display(Name = "Target Value (£)")]
        public decimal TargetValue { get; init; }

        [Display(Name = "Timescale (Years)")]
        public int Timescale { get; init; }

        [Display(Name = "Risk Level")]
        public RiskLevel RiskLevel { get; init; }
    }
}
