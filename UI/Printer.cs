namespace Game.UI;

internal class Printer
{
    public static void PrintLine(string str) =>
        Console.WriteLine(str);

    public static void PrintLine() =>
        Console.WriteLine();

    public static void Print(string str) =>
        Console.Write(str);

    public static void PrintChar(char ch) =>
        Console.Write(ch);

    public static void PrintSuccess(string str) =>
        PrintColor(str, ConsoleColor.Green);

    public static void PrintError(string str) =>
        PrintColor(str, ConsoleColor.Red);

    public static void PrintCommand(string str) =>
        PrintColor(str, ConsoleColor.Blue);

    public static void Clear() =>
        Console.Clear();

    private static void PrintColor(string str, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        PrintLine(str);
        Console.ResetColor();
    }
}
