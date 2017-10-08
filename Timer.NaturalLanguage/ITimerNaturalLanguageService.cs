namespace Timer.NaturalLanguage
{
    public interface ITimerNaturalLanguageService
    {
        AnalysedSentence Analyse(string sentence);
    }
}