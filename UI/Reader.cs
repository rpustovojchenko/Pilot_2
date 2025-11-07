using System.Text;
using Game.Engine;
using Game.Properties;

namespace Game.UI;

internal class Reader
{
    private const int Seconds = GameConstants.InputTimeSeconds;
    private static volatile bool _timeExpired = false;

    public static string ReadLineToLower() =>
        Console.ReadLine()?.ToLower() ?? string.Empty;
    

    public static string ReadLine() =>
        Console.ReadLine() ?? string.Empty;

    public static ConsoleKey ReadKey() =>
        Console.ReadKey(true).Key;

    public static string? ReadLineWithTimer()
    {
        var sb = new StringBuilder();
        _timeExpired = false;

        System.Timers.Timer timer = new(Seconds * 1000)
        {
            AutoReset = false
        };

        timer.Elapsed += (s, e) =>
        {
            _timeExpired = true;
            Printer.PrintError($"\n\n{Res.TimeOverError}");
        };

        timer.Start();

        while (!_timeExpired)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter) break;
                if (key.Key == ConsoleKey.Backspace)
                {
                    if (sb.Length > 0)
                    {
                        sb.Remove(sb.Length - 1, 1);
                        Console.Write("\b \b");
                    }
                    continue;
                }
                if (!char.IsControl(key.KeyChar))
                {
                    sb.Append(key.KeyChar);
                    Printer.PrintChar(key.KeyChar);
                }
            }
            Thread.Sleep(50);
        }
        timer.Stop();

        return _timeExpired ? null : sb.ToString();
    }
}