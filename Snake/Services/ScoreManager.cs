using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;



namespace Snake.Services
{
    public static class ScoreManager
    {
        private static string filePath = @"C:\Users\Пользователь\Desktop\scores.json";

        public static void SaveScore(int score, bool hardMode)
        {
            Dictionary<string, int> scores;

            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                scores = JsonSerializer.Deserialize<Dictionary<string, int>>(json);
            }
            else
            {
                scores = new Dictionary<string, int>();
            }

            var key = hardMode ? "hard" : "classic";

            if (!scores.ContainsKey(key) || score > scores[key])
                scores[key] = score;

            File.WriteAllText(filePath, JsonSerializer.Serialize(scores, new JsonSerializerOptions { WriteIndented = true }));
        }

        public static int GetHighScore(bool hardMode)
        {
            if (!File.Exists(filePath)) return 0;

            var json = File.ReadAllText(filePath);
            var scores = JsonSerializer.Deserialize<Dictionary<string, int>>(json);

            var key = hardMode ? "hard" : "classic";
            return scores.ContainsKey(key) ? scores[key] : 0;
        }
    }
}
