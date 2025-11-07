using Game.Data.Models;


namespace Game.Data;

internal class ScoreManager
{
    private static LinkedList<Score> _allSores = new();

    public ScoreManager(LinkedList<Score> scores)
    {
        _allSores = scores;
    }

    public void UpdateOrCreate(Score score)
    {
        var sc = _allSores.Find(score);

        if (sc != null)
        {
            Score temp = new()
            {
                FirstPlayer = score.FirstPlayer,
                SecondPlayer = score.SecondPlayer,
                FirstPlayerScore = score.FirstPlayerScore + sc.Value.FirstPlayerScore,
                SecondPlayerScore = score.SecondPlayerScore + sc.Value.SecondPlayerScore
            };
            _allSores.AddBefore(sc, temp);
            _allSores.Remove(sc);
        }
        else _allSores.AddLast(score);

        JSON.Serialize(_allSores);
    }

    public static LinkedList<Score> GetScores() => _allSores;
}