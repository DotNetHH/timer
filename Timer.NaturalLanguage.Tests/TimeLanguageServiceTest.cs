using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Timer.Abstractions;
using Timer.NaturalLanguage;

namespace NaturalLanguage.Tests
{
    [TestClass]
    public class TimeLanguageServiceTest
    {
        [TestMethod]
        public void TestSimpleCreate()
        {
            // der test ist falsch, muss an die business logik angepasst werden
            var service = new TimerNaturalLanguageService();
            var result = service.Analyse("Erstelle eine Zeiterfassung von 12:23 bis 14:13 auf das Ticket INC134");


            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result,typeof(StartTaskCommand));
            Assert.AreEqual("INC134", result.TicketId.ToUpper()); 
            Assert.AreEqual(DateTime.Parse("12:23"), result.TimeStamp);
        }

        [TestMethod]
        public void TestSimpleStop()
        {
            var service = new TimerNaturalLanguageService();
            var result = service.Analyse("Beende Zeiterfassung für das Ticket INC1235");


            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(StopTaskCommand));
            Assert.AreEqual("INC1235", result.TicketId.ToUpper());
        }

        [TestMethod]
        public void TestSimpleStart()
        {
            var service = new TimerNaturalLanguageService();
            var result = service.Analyse("Starte Zeiterfassung für das Ticket INC134");


            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(StartTaskCommand));
            Assert.AreEqual("INC134", result.TicketId.ToUpper());
        }
    }
}
