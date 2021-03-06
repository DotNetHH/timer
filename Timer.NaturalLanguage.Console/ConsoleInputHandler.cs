﻿using MJNsoft.Base.DependencyInjection.Abstractions;
using System;

namespace Timer.NaturalLanguage.Console
{
    [AutoRegister]
    public class ConsoleInputHandler : IConsoleInputHandler
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