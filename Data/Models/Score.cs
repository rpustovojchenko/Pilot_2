namespace Game.Data.Models;

struct Score
{
    public string FirstPlayer { get; set; }
    public string SecondPlayer { get; set; }
    public int FirstPlayerScore { get; set; }
    public int SecondPlayerScore { get; set; }

    public void SetPlayers(string[] players)
    {
        FirstPlayer = players[0];
        SecondPlayer = players[1];
        FirstPlayerScore = 0;
        SecondPlayerScore = 0;
    }

    public void SetWinner(string winner)
    {
        if (winner == FirstPlayer) FirstPlayerScore++;
        else SecondPlayerScore++;
    }

    public override bool Equals(object? obj)
    {
        if (obj is Score score)
        {
            return FirstPlayer == score.FirstPlayer && SecondPlayer == score.SecondPlayer;
        }
        return false;
    }

    public override int GetHashCode() =>
        HashCode.Combine(FirstPlayer, SecondPlayer);
}