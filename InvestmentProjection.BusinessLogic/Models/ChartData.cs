using System.Collections.Generic;

namespace InvestmentProjection.BusinessLogic.Models
{
    public class ChartData
    {
        public IEnumerable<int> Labels { get; set; }

        public IEnumerable<GraphDataSet> TotalInvested { get; set; }

        public IEnumerable<GraphDataSet> Growth { get; set; }
    }
}
