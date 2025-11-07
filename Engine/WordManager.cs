using Game.Utils.Validator;


namespace Game.Engine;

internal class WordManager
{
    private string _startWord = string.Empty;
    private Dictionary<char, int> _letterCounts = new();
    private static LinkedList<string> _usedWords = new();

    public void SetStartWord(string word)
    {
        _startWord = word;
        CalculateLetterCounts();
        _usedWords.Clear();
        _usedWords.AddLast(word);
    }

    private void CalculateLetterCounts()
    {
        _letterCounts.Clear();
        foreach (var ch in _startWord)
        {
            if (!_letterCounts.ContainsKey(ch)) _letterCounts.Add(ch, 1);
            else _letterCounts[ch]++;
        }
    }

    public static LinkedList<string> GetUsedWords() => _usedWords;

    public bool CheckStartWord(string word) =>
        WordValidator.IsStartWordValid(word);

    public bool CheckPlayerWord(string word) =>
        WordValidator.IsPlayerWordValid(word, _startWord, _letterCounts, _usedWords);

    public void AddWord(string word) => _usedWords.AddLast(word);

    public void Reset()
    {
        _usedWords.Clear();
        _letterCounts.Clear();
        _startWord = string.Empty;
    }
}