using System;
using System.Collections.ObjectModel;
using System.IO;
using InvestmentProjection.BusinessLogic.Models;
using InvestmentProjection.BusinessLogic.Models.Requests;
using Newtonsoft.Json;

namespace InvestmentProjection.BusinessLogic.Handlers.CalculationsHandler
{
    public class CalculationsHandler
    {
        public ChartData GetChartData(CalculateChartDataRequest request)
        {
            var totalInvestedDataSet = GenerateTotalInvsetmentDataSet(request.LumpSumInvestment, request.MonthlyInvestment, request.Timescale);
            var growthDataSet = GenerateGrowth(totalInvestedDataSet, request.RiskLevel);

            return new ChartData
            {
                Labels = GetYears(request.Timescale),
                TotalInvested = totalInvestedDataSet,
                Growth = growthDataSet,
            };
        }

        private Collection<GraphDataSet> GenerateGrowth(Collection<GraphDataSet> totalInvestedDataSet, RiskLevel riskLevel)
        {
            var growthDataSets = new Collection<GraphDataSet>();

            foreach (var currentInvestedDataPoint in totalInvestedDataSet)
            {
                var growth = CalculateGrowth(riskLevel, currentInvestedDataPoint.Y);

                growthDataSets.Add(new GraphDataSet
                {
                    X = currentInvestedDataPoint.X,
                    Y = growth
                });
            }

            return growthDataSets;
        }

        public Collection<int> GetYears(int timeScale)
        {
            var result = new Collection<int>();
            for (int i = 0; i < GetNumberOfYearsToDisplay(timeScale); i++)
            {
                result.Add(i);
            }
            return result;
        }

        private Collection<GraphDataSet> GenerateTotalInvsetmentDataSet(decimal lumpSum, decimal monthlyInvestment, int timeScale)
        {
            var startDate = DateTime.Today;
            var endDate = DateTime.Today.AddYears(GetNumberOfYearsToDisplay(timeScale));

            var numberOfPoints = (endDate.Year - startDate.Year) * 12 + startDate.Month - endDate.Month;

            var currentInvestment = lumpSum;

            var result = new Collection<GraphDataSet>()
            {
                new GraphDataSet { X = 0, Y = currentInvestment }
            };

            for (int i = 0; i < numberOfPoints; i++)
            {
                currentInvestment += monthlyInvestment;
                result.Add(new GraphDataSet
                {
                    X = (i + 1) / 12m,
                    Y = currentInvestment
                });
            }

            return result;
        }

        public int GetNumberOfYearsToDisplay(int timeScale) => (int)(timeScale + Math.Ceiling(timeScale * 0.2m));

        private decimal CalculateGrowth(RiskLevel risklevel, decimal currentInvestment)
        {
            var growthFiguresContent = File.ReadAllTextAsync("growth.json").Result;
            var growthFigures = JsonConvert.DeserializeObject<GrowthConfig>(growthFiguresContent);

            switch (risklevel)
            {
                case RiskLevel.Low:
                    return currentInvestment + currentInvestment * (growthFigures.low / 100);
                case RiskLevel.Medium:
                    return currentInvestment + currentInvestment * (growthFigures.med / 100);
                case RiskLevel.High:
                    return currentInvestment + currentInvestment * (growthFigures.high / 100);
            }

            return currentInvestment;
        }

        private class GrowthConfig
        {
            public decimal low { get; set; }
            public decimal med { get; set; }
            public decimal high { get; set; }
        }
    }
}
