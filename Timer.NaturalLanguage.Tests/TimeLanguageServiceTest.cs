using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Timer.NaturalLanguage;

namespace NaturalLanguage.Tests
{
    [TestClass]
    public class TimeLanguageServiceTest
    {
        [TestMethod]
        public void TestSimpleCreate()
        {
            var service = new TimerNaturalLanguageService();
            var result = service.Analyse("Erstelle eine Zeiterfassung von 12:23 bis 14:13 auf das Ticket INC134");

            Assert.IsNotNull(result);
            Assert.AreEqual("INC123", result.TicketId.ToUpper()); 
            Assert.AreEqual(DateTime.Parse("12:23"), result.TimeStamp);
        }
    }
}
