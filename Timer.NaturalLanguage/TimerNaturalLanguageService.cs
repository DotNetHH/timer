using MJNsoft.Base.DependencyInjection.Abstractions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using Timer.Business.Abstractions;

namespace Timer.NaturalLanguage
{
    [AutoRegister]
    public class TimerNaturalLanguageService : ITimerNaturalLanguageService
    {
        private string _url = "https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/50980763-63b2-42c9-9abf-d38e0acc0c32?subscription-key=2563750847fa4ae4919bde9f07acf981&timezoneOffset=0&verbose=true&q=";

        public AnalysedSentence Analyse(string sentence)
        {
            var json = LoadJson(sentence);
            #region debug
            var json2 = @"{
  'query': 'Erstelle eine Zeiterfassung von 12:23 bis 14:13 auf das Ticket INC134',
  'topScoringIntent': {
                'intent': 'CreareCompleteTimeRecording',
    'score': 0.96630013
  },
  'intents': [
    {
      'intent': 'CreareCompleteTimeRecording',
      'score': 0.96630013
    },
    {
      'intent': 'None',
      'score': 0.05849345
    },
    {
      'intent': 'CreateInterrupt',
      'score': 7.13342558E-07
    }
  ],
  'entities': [
    {
      'entity': 'inc134',
      'type': 'Incident',
      'startIndex': 63,
      'endIndex': 68,
      'score': 0.769762635
    },
    {
      'entity': 'erstelle',
      'type': 'Action',
      'startIndex': 0,
      'endIndex': 7,
      'score': 0.9638629
    },
    {
      'entity': 'zeit erfassung',
      'type': 'Activity',
      'startIndex': 14,
      'endIndex': 26,
      'score': 0.908216357
    },
    {
      'entity': '14:13',
      'type': 'To',
      'startIndex': 42,
      'endIndex': 46,
      'score': 0.9741547
    },
    {
      'entity': '12:23',
      'type': 'From',
      'startIndex': 32,
      'endIndex': 36,
      'score': 0.8447987
    }
  ]
}";
            #endregion
            var analysedSentence = Parse(json);
            return analysedSentence;
        }

        private AnalysedSentence Parse(string json)
        {
            JObject commandAsLanguage = JObject.Parse(json);

            var intentAsString = commandAsLanguage["intents"].First.Value<string>("intent");
            var scoreAsString = commandAsLanguage["intents"].First.Value<string>("score");
            var score = double.Parse(scoreAsString, CultureInfo.InvariantCulture);

            if (score < 0.9)
            {
                throw new Exception("Unsicherheit: " + score);
            }

            if (!Enum.TryParse(intentAsString, out AnalysedSentenceIntent intent))
            {
                throw new Exception("Intent not supported " + intentAsString);
            }

            var analysedSentence = new AnalysedSentence();
            analysedSentence.Intent = intent;
            analysedSentence.Entities = ParseEntities(commandAsLanguage);
            analysedSentence.Score = score;

            return analysedSentence;
        }

        private static Dictionary<AnalysedSentenceEntity, string> ParseEntities(JObject commandAsLanguage)
        {
            var dictionary = new Dictionary<AnalysedSentenceEntity, string>();

            // alle type und entities sammeln
            var entitiesAsList = from e in commandAsLanguage["entities"]
                                 select new { Key = (string)e["type"], Value = (string)e["entity"] };

            foreach (var entity in entitiesAsList)
            {
                // keinen fehler schmeißen, wenn unbekannt
                if (!Enum.TryParse(entity.Key, out AnalysedSentenceEntity analysedSentenceEntity))
                {
                    Console.WriteLine("Entity Type not supported: {0}", entity.Key);
                }
                else
                {
                    dictionary[analysedSentenceEntity] = entity.Value;
                }
            }

            return dictionary;
        }

        private string LoadJson(string Sentence)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = client.GetAsync(_url + Sentence); //evil
                response.Wait();

                var data = response.Result.Content.ReadAsStringAsync();
                data.Wait();

                //Console.WriteLine(data.Result);
                return data.Result;
            }
        }
    }
}
