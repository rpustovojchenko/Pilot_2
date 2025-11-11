using Game.Properties;
using Game.Data;
using Game.UI;
using Game.Utils.Lokalization;


namespace Game.Engine;

internal class Game
{
    private PlayerManager _playerManager;
    private WordManager _wordManager;
    private ScoreManager _scoreManager;
    private Menu _menu;
    private Language _language;
    private bool _isGameEnd;

    private string? _lastPlayer;

    public Game()
    {
        _language = new(0);
        _playerManager = new();
        _wordManager = new();
        _scoreManager = new(JSON.Deserialize());
        _menu = new();
        _isGameEnd = true;

        AppDomain.CurrentDomain.ProcessExit += OnProcessExit;
    }

    public void Run()
    {
        while (true)
        {
            int selected = _menu.ShowMenu(Res.menu);

            switch (selected)
            {
                case 0: PlayGame(); break;
                case 1: MenuOptions.PrintRules(); break;
                case 2: MenuOptions.ChoosePlayerName(_playerManager); break;
                case 3:
                    MenuOptions.ChooseLanguage(_menu, _language);
                    _menu.UpdateItems(_menu.GetMenuItems());
                    break;
                case 4: return;
            }

            Printer.PrintLine(Res.PressAnyKey);
            Reader.ReadKey();
        }
    }

    private void PlayGame()
    {
        _isGameEnd = false;
        _playerManager.SetNames();

        var (FirstPlayerScore, SecondPlayerScore) =
            _scoreManager.CreateScoresForPlayers(_playerManager.AllPlayers);

        _scoreManager.CreateScore(FirstPlayerScore);
        _scoreManager.CreateScore(SecondPlayerScore);

        Printer.Clear();
        Printer.Print($"{_playerManager.CurrentPlayer} {Res.EnterStartWord}");
        string startWord = Reader.ReadLineToLower().Trim();

        if (!_wordManager.CheckStartWord(startWord)) return;
        _lastPlayer = _playerManager.CurrentPlayer;

        Printer.PrintLine();

        _wordManager.SetStartWord(startWord);
        _playerManager.SwitchPlayer();

        while (true)
        {
            Printer.Print($"{_playerManager.CurrentPlayer} {Res.EnterWord}");
            string? playerWord;

            if ((playerWord = Reader.ReadLineWithTimer()) == null) break;

            Printer.PrintLine("\n");

            if (Command.IsCommand(playerWord))
            {
                Command.Execute(playerWord, _playerManager);
                continue;
            }

            if (!_wordManager.CheckPlayerWord(playerWord))
            {
                _wordManager.Reset();
                break;
            }

            _wordManager.AddWord(playerWord);
            _lastPlayer = _playerManager.CurrentPlayer;
            _playerManager.SwitchPlayer();
        }

        _isGameEnd = true;

        _playerManager.SwitchPlayer();
        Printer.PrintSuccess($"\n{Res.Won} {_playerManager.CurrentPlayer} {Res.WonEn}!\n");

        _scoreManager.UpdateScore(_playerManager.CurrentPlayer, FirstPlayerScore,
            SecondPlayerScore, _playerManager.AllPlayers);

        _playerManager.ResetPlayer();
    }

    private void OnProcessExit(object? sender, EventArgs e)
    {
        if (_isGameEnd) return;

        if (!string.IsNullOrEmpty(_lastPlayer))
        {
            var (FirstPlayerScore, SecondPlayerScore) =
                _scoreManager.CreateScoresForPlayers(_playerManager.AllPlayers);

            _scoreManager.UpdateScore(_lastPlayer, FirstPlayerScore,
                SecondPlayerScore, _playerManager.AllPlayers);
        }
    }
}