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
        public void TestSimpleStop()
        {
            var service = new TimerNaturalLanguageService();
            var result = service.Analyse("Beende Zeiterfassung für das Ticket INC1235");


            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(AnalysedSentence));
            Assert.AreEqual("INC1235", result.GetEntityOrEmpty(AnalysedSentenceEntity.Incident).ToUpper());
        }

        [TestMethod]
        public void TestSimpleStart()
        {
            var service = new TimerNaturalLanguageService();
            var result = service.Analyse("Starte Zeiterfassung für das Ticket INC134");


            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(AnalysedSentence));
            Assert.AreEqual(AnalysedSentenceIntent.StartTimeRecordingNow, result.Intent);
            Assert.AreEqual("INC134", result.GetEntityOrEmpty(AnalysedSentenceEntity.Incident).ToUpper());
        }

        [TestMethod]
        public void TestStartWithDectiption()
        {
            var service = new TimerNaturalLanguageService();
            var result = service.Analyse("Starte Zeiterfassung mit der Beschreibung: Arbeit Arbeit");


            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(AnalysedSentence));
            Assert.AreEqual(AnalysedSentenceIntent.StartTimeRecordingNow, result.Intent);
            Assert.AreEqual("ARBEIT ARBEIT", result.GetEntityOrEmpty(AnalysedSentenceEntity.Description).ToUpper());
        }
    }
}
