namespace Game.UI;

internal class Menu
{
    private string[] _items;

    public Menu(string[] items)
    {
        _items = items;
    }

    public void UpdateItems(string[] items) =>_items = items;

    public int ShowMenu(string title)
    {
        int selected = 0;

        void DisplayMenu()
        {
            Printer.Clear();
            Printer.PrintLine($"=== {title} ===\n");
            for (int i = 0; i < _items.Length; i++)
            {
                if (i == selected) Printer.PrintSuccess($"> {_items[i]}");
                else Printer.PrintLine("  " + _items[i]);
            }
        }

        while (true)
        {
            DisplayMenu();
            var key = Reader.ReadKey();

            if (key == ConsoleKey.UpArrow) selected = (selected - 1 + _items.Length) % _items.Length;
            else if (key == ConsoleKey.DownArrow) selected = (selected + 1) % _items.Length;
            else if (key == ConsoleKey.Escape) return -1;
            else if (key == ConsoleKey.Enter)
            {
                Printer.Clear();
                Printer.PrintLine($"=== {_items[selected]} ===\n");
                return selected;
            }
        }
    }
}