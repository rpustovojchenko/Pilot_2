using System.Globalization;
using System.Text;

namespace Game.Utils.Lokalization;

internal class Language
{
    private int _language;

    public Language(int language)
    {
        _language = language;
        SetLanguage(language);
    }

    public void SetLanguage(int lang)
    {
        _language = lang;
        CultureInfo culture = _language == 0
            ? new CultureInfo("ru-Ru")
            : new CultureInfo("en-US");
        Thread.CurrentThread.CurrentUICulture = culture;
        Console.OutputEncoding = Encoding.UTF8;
    }
}