using Game.Data;
using Game.Data.Models;
using Game.Properties;
using Game.UI;

namespace Game.Engine;

internal class Command
{
    public static bool IsCommand(string command) =>
        command.StartsWith('/');

    public static void Execute(string command, in PlayerManager player)
    {
        switch (command)
        {
            case "/show-words": ShowWords(); break;
            case "/score": ShowScore(player); break;
            case "/total-score": ShowTotalScore(); break;
            default: Printer.PrintError($"{Res.WrongCommand}\n"); break;
        }
    }

    private static void ShowWords()
    {
        Printer.PrintCommand(string.Join(", ", WordManager.GetUsedWords()));
        Printer.PrintLine();
    }

    private static void ShowScore(in PlayerManager player)
    {
        Printer.PrintCommand(string.Format("{0,-20} {1,-5}\n", Res.Player, Res.Score));
        foreach (var score in ScoreManager.GetScores())
            if (score.Player == player.AllPlayers[0] || score.Player == player.AllPlayers[1])
                PrintScore(score);
    }

    private static void ShowTotalScore()
    {
        Printer.PrintCommand(string.Format("{0,-20} {1,-5}\n", Res.Player, Res.Score));
        foreach (var score in ScoreManager.GetScores())
            PrintScore(score);
    }

    private static void PrintScore(Score score) =>
        Printer.PrintCommand(string.Format("{0,-20} {1,-5}\n", score.Player, score.PlayerScore));
}