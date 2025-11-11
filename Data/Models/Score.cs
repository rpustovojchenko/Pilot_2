namespace Game.Data.Models;

struct Score
{
    public string Player { get; set; }
    public int PlayerScore { get; set; }

    public void SetPlayer(string player)
    {
        Player = player;
        PlayerScore = 0;
    }

    public override bool Equals(object? obj)
    {
        if (obj is Score score)
        {
            return Player == score.Player;
        }
        return false;
    }

    public override int GetHashCode() =>
        HashCode.Combine(Player);
}