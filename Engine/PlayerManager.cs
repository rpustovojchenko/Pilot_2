using Game.Properties;


namespace Game.Engine;

internal class PlayerManager
{
    public string[] AllPlayers { get; private set; }
    public string CurrentPlayer { get; private set; }
    private int _currentPlayerIndex;
    private bool IsDefaultNames = true;

    public PlayerManager()
    {
        AllPlayers = [Res.Player1, Res.Player2];
        CurrentPlayer = AllPlayers[0];
        ResetPlayer();
    }

    public void SwitchPlayer()
    {
        _currentPlayerIndex = _currentPlayerIndex == 1 ? 0 : 1;
        ChangePlayer();
    }

    public void ResetPlayer()
    {
        _currentPlayerIndex = 0;
        ChangePlayer();
    }

    public void UpdatePlayersName(string name1, string name2)
    {
        IsDefaultNames = false;
        AllPlayers[0] = name1.Trim();
        AllPlayers[1] = name2.Trim();
        ChangePlayer();
    }

    private void ChangePlayer() =>
        CurrentPlayer = AllPlayers[_currentPlayerIndex];

    public void SetNames()
    {
        if (IsDefaultNames)
            AllPlayers = GetPlayersName();
        ChangePlayer();
    }
    private string[] GetPlayersName() =>
        [Res.Player1, Res.Player2];
}
