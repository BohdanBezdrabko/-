public class PlayerStats
{
    public string PlayerName { get; }
    public int Wins { get; private set; }
    public int Losses { get; private set; }
    public int Draws { get; private set; }
    public int TotalGames => Wins + Losses + Draws;

    public PlayerStats(string playerName, string gameResult)
    {
        PlayerName = playerName;
        UpdateStats(gameResult);
    }

    public void UpdateStats(string gameResult)
    {
        switch (gameResult)
        {
            case "Win":
                Wins++;
                break;
            case "Lose":
                Losses++;
                break;
            case "Draw":
                Draws++;
                break;
            default:
                Console.WriteLine($"Unknown game result: {gameResult}");
                break;
        }
    }

    public override string ToString()
    {
        return $"{PlayerName}\t   {Wins}\t   {Losses}\t   {Draws}\t   {TotalGames}";
    }
}
