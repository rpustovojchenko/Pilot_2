using Game.Data.Models;
using Game.Engine;


namespace Game.Data;

internal class ScoreManager
{
    private static LinkedList<Score> _allSores = new();

    public ScoreManager(LinkedList<Score> scores)
    {
        _allSores = scores;
    }

    private void UpdateOrCreate(Score score)
    {
        var sc = _allSores.Find(score);

        if (sc != null)
        {
            Score temp = new()
            {
                Player = score.Player,
                PlayerScore = sc.Value.PlayerScore + 1
            };
            _allSores.AddBefore(sc, temp);
            _allSores.Remove(sc);
        }
        else _allSores.AddLast(score);

        JSON.Serialize(_allSores);
    }

    public void CreateScore(Score score)
    {
        var sc = _allSores.Find(score);

        if (sc == null)
        {
            _allSores.AddLast(score);
            JSON.Serialize(_allSores);
        }
    }

    public void UpdateScore(string player, Score FirstPlayerScore, Score SecondPlayerScore, in string[] allPlayers) =>
        UpdateOrCreate(player == allPlayers[0]
            ? FirstPlayerScore : SecondPlayerScore);

    public (Score firstPlayerScore, Score secondPlayerScore) CreateScoresForPlayers(in string[] allPlayers)
    {
        Score firstPlayerScore = new();
        Score secondPlayerScore = new();

        firstPlayerScore.SetPlayer(allPlayers[0]);
        secondPlayerScore.SetPlayer(allPlayers[1]);

        return (firstPlayerScore, secondPlayerScore);
    }

    public static LinkedList<Score> GetScores() => _allSores;
}