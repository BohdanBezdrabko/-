using System;
using System.IO;
using System.Linq;

public class Program
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        string usersPath = Path.Combine(Directory.GetCurrentDirectory(), "Users.txt");
        string historyPath = Path.Combine(Directory.GetCurrentDirectory(), "History.txt");
        User user = null;
        InitializeDatabase(usersPath);
        InitializeDatabase(historyPath);

        do
        {
            if (user == null)
            {
                string credentials;
                do
                {
                    credentials = UserInterface.GetUserCredentials();
                } while (credentials.Split(' ').Length != 2);

                user = new User(credentials.Split(' ')[0], credentials.Split(' ')[1]);
            }

            UserInterface.ShowMenu(user);
            switch (UserInterface.GetMenuChoice())
            {
                case "1":
                    StreamWriter sw = File.AppendText(historyPath);
                    TicTacToeGame game = new TicTacToeGame();
                    game.StartGame();
                    sw.WriteLine($"{user.GetName()} {game} {DateTime.Now:yyyy-MM-dd_HH:mm:ss}");
                    sw.Close();
                    break;
                case "2":
                    ShowRatings(historyPath);
                    break;
                case "3":
                    ShowGameHistory(user.GetName(), historyPath);
                    break;
                case "4":
                    user = null;
                    Console.WriteLine("Logged out successfully.");
                    break;
                case "5":
                    Console.WriteLine("Goodbye! Thanks for playing!");
                    return;
                default:
                    Console.Clear();
                    break;
            }
        } while (true);
    }

    private static void InitializeDatabase(string path)
    {
        if (!File.Exists(path))
        {
            try
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.Close();
                }
                Console.WriteLine($"Database created: {path}");
            }
            catch (System.Exception e)
            {
                Console.WriteLine($"Error creating database: {e.Message}");
            }
        }
    }

    private static void ShowRatings(string historyPath)
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
        string scoreboard = "Name\tWins Losses Draws\n";
        int i = 1;

        foreach (var playerStat in sortedPlayers)
        {
            scoreboard += $"{i++}.{playerStat}\n";
        }

        Console.WriteLine(scoreboard);
    }

    private static void ShowGameHistory(string userName, string historyPath)
    {
        Console.Clear();
        StreamReader sr = new StreamReader(historyPath);
        string line = sr.ReadLine();
        string history = $"Game history for {userName}:\nResult\t\tDate\n";

        do
        {
            if (line.Split(' ')[0] == userName)
            {
                history += $"{line.Split(' ')[1]}\t\t{line.Split(" ")[2]}\n";
            }

            line = sr.ReadLine();
        } while (line != null);

        sr.Close();
        Console.WriteLine(history);
    }
}
