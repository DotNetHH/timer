using System;
using Newtonsoft.Json;
using Timer.Business.Abstractions;

namespace Timer.NaturalLanguage
{
    public class SentenceHandler : ISentenceHandler
    {
        private readonly TimerNaturalLanguageService _naturalLanguageService;
        private readonly ICommandManager _commandManager;
        private readonly IReaderManager _readerManager;

        public SentenceHandler(TimerNaturalLanguageService naturalLanguageService, ICommandManager commandManager, IReaderManager readerManager)
        {
            _naturalLanguageService = naturalLanguageService;
            _commandManager = commandManager;
            _readerManager = readerManager;
        }

        public void Handle(string sentence)
        {
            var analysedsentence = _naturalLanguageService.Analyse(sentence);

            System.Console.WriteLine("Erkannt wurde: ");
            System.Console.WriteLine(JsonConvert.SerializeObject(analysedsentence));

            switch (analysedsentence.Intent)
            {
                case AnalysedSentenceIntent.StartTimeRecordingNow:
                    Start(analysedsentence);
                    break;
                case AnalysedSentenceIntent.StopTimeRecordingNow:
                    Stop(analysedsentence);
                    break;
                case AnalysedSentenceIntent.ShowTimeRecording:
                    Show();
                    break;
            }
        }

        private void Start(AnalysedSentence analysedsentence)
        {
            var command = new StartTaskCommand()
            {
                TimeStamp = DateTime.Now,
                Description = analysedsentence.GetEntityOrEmpty(AnalysedSentenceEntity.Description),
                TicketId = analysedsentence.GetEntityOrEmpty(AnalysedSentenceEntity.Incident)
            };
            _commandManager.AddWriterCommand(command);
        }

        private void Stop(AnalysedSentence analysedsentence)
        {
            var  command = new StopTaskCommand()
            {
                TimeStamp = DateTime.Now,
                Description = analysedsentence.GetEntityOrEmpty(AnalysedSentenceEntity.Description),
                TicketId = analysedsentence.GetEntityOrEmpty(AnalysedSentenceEntity.Incident)
            };
            _commandManager.AddWriterCommand(command);
        }


        private void Show()
        {
            foreach (var command in this._readerManager.GetAll())
            {
                System.Console.WriteLine(command.GetType().Name + " " + JsonConvert.SerializeObject(command));
            }
        }
    }
}