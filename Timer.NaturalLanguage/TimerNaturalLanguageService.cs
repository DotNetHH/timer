using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;
using Timer.Abstractions;
using Timer.Data;

namespace Timer.NaturalLanguage
{
    public class TimerNaturalLanguageService
    {
        private string _url = "https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/50980763-63b2-42c9-9abf-d38e0acc0c32?subscription-key=2563750847fa4ae4919bde9f07acf981&timezoneOffset=0&verbose=true&q=";

        public WriterCommand Analyse(string Sentence)
        {
            var json = LoadJson(Sentence);
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
            var command = Parse(json);
            return command;
        }

        private WriterCommand Parse(string json)
        {
            JObject commandAsLanguage = JObject.Parse(json);

            var intent = commandAsLanguage["intents"].First.Value<string>("intent");

            //var incident = GetEntity(commandAsLanguage, "Incident");

            switch (intent)
            {
                case "CreareCompleteTimeRecording":
                    return BuildCreateCommand(commandAsLanguage);
                case "StartTimeRecordingNow":
                    return BuildStart(commandAsLanguage);
                case "StopTimeRecordingNow":
                    return BeginEnd(commandAsLanguage);
            }

            return null;
        }

        private WriterCommand BuildStart(JObject commandAsLanguage)
        {
            return new StartTaskCommand()
            {
                TimeStamp = DateTime.Now,
                Description = GetEntity(commandAsLanguage, "Description"),
                TicketId = GetEntity(commandAsLanguage, "Incident")
            };
        }

        private WriterCommand BeginEnd(JObject commandAsLanguage)
        {
            return new StopTaskCommand()
            {
                TimeStamp = DateTime.Now,
                Description = GetEntity(commandAsLanguage, "Description"),
                TicketId = GetEntity(commandAsLanguage, "Incident")
            };
        }

        private StartTaskCommand BuildCreateCommand(JObject commandAsLanguage)
        {
            var command = new StartTaskCommand();

            var fromAsString = GetEntity(commandAsLanguage, "From");
            if (!string.IsNullOrWhiteSpace(fromAsString))
            {
                var from = DateTime.Parse(fromAsString);
                command.TimeStamp = from;
            }

            command.TicketId = GetEntity(commandAsLanguage, "Incident");

            return command;
        }

        private string GetEntity(JObject commandAsLanguage, string Type)
        {
            var entity = (from e in commandAsLanguage["entities"]
                          where (string)e["type"] == Type
                          select (string)e["entity"]).FirstOrDefault();

            return entity;
        }

        private string LoadJson(string Sentence)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = client.GetAsync(_url + Sentence); //evil
                response.Wait();

                var data = response.Result.Content.ReadAsStringAsync();
                data.Wait();

                Console.WriteLine(data.Result);
                return data.Result;
            }
        }
    }
}
