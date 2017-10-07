using System;
using System.Linq;
using Newtonsoft.Json;

namespace Timer.NaturalLanguage.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Zeiterfassung gestartet!");
            System.Console.WriteLine("Was möchten Sie tun?");
            System.Console.WriteLine("");
            System.Console.WriteLine("Beispiel: Starte Zeiterfassung");

            var service = new TimerNaturalLanguageService();

            bool isRunning = true;
            while (isRunning)
            {
                var sentence = System.Console.ReadLine();

                if(string.IsNullOrWhiteSpace(sentence))
                {
                    //nothing
                }
                else if (sentence == "exit")
                {
                    isRunning = false;
                }
                else
                {
                    try
                    {
                        var command = service.Analyse(sentence);
                        System.Console.WriteLine(command.GetType().Name + " " + JsonConvert.SerializeObject(command));
                    }
                    catch (Exception e)
                    {
                        System.Console.WriteLine("Exception: "+e.Message);
                    }
                }
            }

            //= args.Aggregate("", (i, j) => i + " " + j);
            
            
        }
    }
}
