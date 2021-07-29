using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using InvestmentProjection.BusinessLogic.Handlers.CalculationsHandler;
using Xunit;

namespace InvestmentProjection.Tests
{
    public class CalculationHandlerTests
    {
        [Fact]
        public void CalculateYears()
        {
            var result = new CalculationsHandler().GetYears(123);

            Assert.IsType<Collection<int>>(result);
        }

        [Fact]
        public void GetNumberOfYearsToDisplay()
        {
            var result = new CalculationsHandler().GetNumberOfYearsToDisplay(123);

            Assert.True(result > 0);
        }
    }
}
