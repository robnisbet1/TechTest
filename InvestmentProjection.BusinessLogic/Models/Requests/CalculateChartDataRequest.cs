namespace InvestmentProjection.BusinessLogic.Models.Requests
{
    public class CalculateChartDataRequest
    {
        public decimal LumpSumInvestment { get; set; }

        public decimal MonthlyInvestment { get; set; }

        public decimal TargetValue { get; set; }

        public int Timescale { get; set; }

        public RiskLevel RiskLevel { get; set; }
    }
}
