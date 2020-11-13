using MentoringApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MentoringApi.Tests
{
    public class WeatherForecastTests
    {
        [Fact]
        public void VerifyIfForecastDateIsBetweenFiveDaysTimeSpan()
        {
            //Mock<ILogger<WeatherForecastController>> loggerMock = new Mock<ILogger<WeatherForecastController>>();

            WeatherForecastController controller = new WeatherForecastController();
            IEnumerable<WeatherForecast> result = controller.Get();
            
            DateTime maxDateScope = DateTime.Now.AddDays(5);
            DateTime minDateScope = DateTime.Now.AddDays(1);

            bool allDatesBetweenExpectedScope = result.All(x => x.Date.Date >= minDateScope.Date && x.Date.Date <= maxDateScope.Date);

            Assert.True(allDatesBetweenExpectedScope);
        }
    }
}
