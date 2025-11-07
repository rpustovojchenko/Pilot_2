using Game.Engine;
using Game.Properties;
using Game.Utils.Lokalization;
using Game.Utils.Validator;

namespace Game.UI;

internal class MenuOptions
{
    public static void ChoosePlayerName(in PlayerManager player)
    {
        string name1;
        string name2;

        do
        {
            Printer.Print(Res.FirstPlayerName);
            name1 = Reader.ReadLine();
        } while (!PlayerNameValidator.IsPlayerNameValid(name1));

        Printer.PrintLine();

        do
        {
            Printer.Print(Res.SecondPlayerName);
            name2 = Reader.ReadLine();
        } while (!PlayerNameValidator.IsPlayerNameValid(name2) || !PlayerNameValidator.IsSecondPlayerNameValid(name1, name2));

        Printer.PrintLine();

        player.UpdatePlayersName(name1, name2);
    }

    public static void PrintRules() =>
        Printer.PrintLine(Res.RulesText);

    public static void ChooseLanguage(in Menu menu, in Language language)
    {
        string[] languages = [Res.Russian, Res.English];
        menu.UpdateItems(languages);
        language.SetLanguage(menu.ShowMenu(Res.Language));
    }
}
