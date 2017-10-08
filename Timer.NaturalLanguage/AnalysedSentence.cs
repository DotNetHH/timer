using System.Collections.Generic;

namespace Timer.NaturalLanguage
{
    public class AnalysedSentence
    {
        public AnalysedSentenceIntent Intent { get; set; }
        public Dictionary<AnalysedSentenceEntity, string> Entities { get; set; }
        public double Score { get; set; }

        public string GetEntityOrEmpty(AnalysedSentenceEntity entity)
        {
            if (Entities != null && Entities.ContainsKey(entity))
            {
                return Entities[entity];
            }

            return string.Empty;
        }
    }
}