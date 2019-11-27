namespace UpcomingSuperStarsOfTheNBA
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class Program
    {
        static void Main(string[] args)
        {
            string filePath = Console.ReadLine();

            string json = string.Empty;
            var players = new List<Player>();

            using (StreamReader r = new StreamReader(filePath))
            {
                json = r.ReadToEnd();
                players = JsonConvert.DeserializeObject<List<Player>>(json);
            }

            int maxYears = int.Parse(Console.ReadLine());
            double minRating = double.Parse(Console.ReadLine());
            string exportPath = Console.ReadLine();

            var filteredPlayers = players
                .Where(p => p.Rating > minRating)
                .Where(p=> p.PlayerSince < maxYears)
                .OrderByDescending(p => p.Rating)
                .ThenBy(p => p.Name)
                .ToList();

            StringBuilder builder = new StringBuilder();
            foreach (var player in filteredPlayers)
            {
                builder.AppendLine($"{player.Name}, {player.Rating:F1}");
            }

            File.WriteAllText(exportPath, builder.ToString());
        }
    }
}
