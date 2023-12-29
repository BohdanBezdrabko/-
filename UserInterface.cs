using System;

public class UserInterface
{
    public static string GetUserCredentials()
    {
        Console.Clear();
        Console.WriteLine("Enter login and password separated by a space for registration or login: ");
        return Console.ReadLine();
    }

    public static void ShowMenu(User user)
    {
        Console.WriteLine($"Current user: {user.GetName()}\nEnter a menu item from the keyboard:\n\n1. Start Tic-Tac-Toe\n2. Ratings\n3. History of my games\n4. Log out\n5. Exit");
    }

    public static string GetMenuChoice()
    {
        return Console.ReadLine();
    }

    public static void ShowRatings(string historyPath)
    {
        Console.Clear();
        Dictionary<string, PlayerStats> playerStats = new Dictionary<string, PlayerStats>();

        foreach (string line in File.ReadAllLines(historyPath))
        {
            if (playerStats.ContainsKey(line.Split(' ')[0]))
            {
                playerStats[line.Split(' ')[0]].UpdateStats(line.Split(' ')[1]);
            }
            else
            {
                playerStats[line.Split(' ')[0]] = new PlayerStats(line.Split(' ')[0], line.Split(' ')[1]);
            }
        }

        var sortedPlayers = playerStats.Values.OrderByDescending(p => p.Wins);
        string scoreboard = "Name\tWins Losses Draws TotalGames\n";
        int i = 1;

        foreach (var playerStat in sortedPlayers)
        {
            scoreboard += $"{i++}.{playerStat}\n";
        }

        Console.WriteLine(scoreboard);
    }
}
