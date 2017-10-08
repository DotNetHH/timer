using System;

namespace Timer.NaturalLanguage.Console
{
    public class ConsoleInputHandler
    {
        private readonly ISentenceHandler _sentenceHandler;

        public ConsoleInputHandler(ISentenceHandler sentenceHandler)
        {
            this._sentenceHandler = sentenceHandler;
        }

        public void Start()
        {

            System.Console.WriteLine("Zeiterfassung gestartet!");
            System.Console.WriteLine("Was möchten Sie tun?");
            System.Console.WriteLine("");
            System.Console.WriteLine("Beispiel: Starte Zeiterfassung");

            bool isRunning = true;
            while (isRunning)
            {
                var sentence = System.Console.ReadLine();

                if (string.IsNullOrWhiteSpace(sentence))
                {
                    //nothing
                }
                else if (sentence == "exit")
                {
                    isRunning = false;
                }
                //else if (sentence == "list")
                //{
                //    foreach (var command in commandManager.GetAll())
                //    {
                //        System.Console.WriteLine(command.GetType().Name + " " + JsonConvert.SerializeObject(command));
                //    }
                //}
                else
                {
                    try
                    {
                        this._sentenceHandler.Handle(sentence);
                    }
                    catch (Exception e)
                    {
                        System.Console.WriteLine("Exception: " + e.Message);
                    }
                }
            }
        }
    }
}