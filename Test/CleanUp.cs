using AlphaPoint_QA.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace AlphaPoint_QA.Test
{
    [Collection("Alphapoint_QA_USER")]
    public class CleanUp : TestBase
    {
        public CleanUp(ITestOutputHelper output) : base(output)
        {

        }

        [Fact]
        public void CleanUpTest()
        {
            try
            {
                AlphaPointWebDriver.DestroyInstanceOfAlphaPointWebDriver(driver);
            }
            catch(Exception e)
            {
                TestProgressLogger.LogError(LogMessage.CleanUpFailedMsg, e);
                throw e;
            }
        }
    }
}
