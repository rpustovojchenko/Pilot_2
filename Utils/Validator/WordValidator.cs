using Game.Engine;
using Game.Properties;
using Game.UI;


namespace Game.Utils.Validator;

internal class WordValidator
{
    public static bool IsStartWordValid(string word)
    {
        if (!(word.Length >= GameConstants.MinStartWordLength && word.Length <= GameConstants.MaxStartWordLength))
        {
            Printer.PrintError($"\n{Res.StartWordLenError}\n");
            return false;
        }

        foreach (char c in word)
        {
            if (!char.IsLetter(c))
            {
                Printer.PrintError($"\n{Res.StartWordPunctuationError}\n");
                return false;
            }
        }

        return true;
    }

    public static bool IsPlayerWordValid(
        string word,
        string startWord,
        in Dictionary<char, int> letterCounts,
        in LinkedList<string> usedWords
        )
    {
        if (string.IsNullOrEmpty(word))
        {
            Printer.PrintError(Res.EmptyStringError);
            return false;
        }

        if (word.Length > startWord.Length)
        {
            Printer.PrintError(Res.PlayerWordLenError);
            return false;
        }

        if (usedWords.Contains(word))
        {
            Printer.PrintError(Res.UsingWordError);
            return false;
        }

        foreach (var ch in word)
        {
            if (!char.IsLetter(ch))
            {
                Printer.PrintError(Res.WordPunctuationError);
                return false;
            }
            if (!letterCounts.ContainsKey(ch))
            {
                Printer.PrintError($"{Res.NoLetterError} {ch}!");
                return false;
            }

            if (letterCounts[ch] < word.Count(c => c == ch))
            {
                Printer.PrintError($"{Res.ManyLettersError} {ch}!");
                return false;
            }
        }

        return true;
    }
}
