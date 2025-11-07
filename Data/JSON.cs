using Game.Data.Models;
using System.Text.Json;

namespace Game.Data;

internal class JSON
{
    private static string PATH = "scores.json";

    private static readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true
    };

    public static void Serialize(in LinkedList<Score> scores)
    {
        string serializedScores = JsonSerializer.Serialize(scores, _options);
        File.WriteAllText(PATH, serializedScores);
    }

    public static LinkedList<Score> Deserialize()
    {
        if (!File.Exists(PATH) || new FileInfo(PATH).Length == 0)
            return new LinkedList<Score>();

        return JsonSerializer.Deserialize<LinkedList<Score>>(File.ReadAllText(PATH)) 
            ?? new LinkedList<Score>();
    }
}