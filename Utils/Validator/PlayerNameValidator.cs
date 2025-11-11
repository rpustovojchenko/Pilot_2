using Game.Properties;
using Game.UI;

namespace Game.Utils.Validator;

internal class PlayerNameValidator
{
    public static bool IsPlayerNameValid(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            Printer.PrintError($"\n{Res.EmptyNameError}\n");
            return false;
        }

        foreach (var ch in name)
        {
            if (!char.IsLetter(ch) && !(ch == ' ') && !char.IsDigit(ch))
            {
                Printer.PrintError($"\n{Res.NotLettersError}\n");
                return false;
            }
        }

        return true;
    }

    public static bool IsSecondPlayerNameValid(string firstPlayerName, string secondPlayerName)
    {
        if (firstPlayerName == secondPlayerName)
        {
            Printer.PrintError($"\n{Res.SameNamesError}\n");
            return false;
        }
        return true;
    }
}
