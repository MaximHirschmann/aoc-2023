// TEMPLATE

using Solutions.Utils;

namespace Solutions.Days.Day_02;

internal class Solution : SolutionBase
{
    internal override int Solve1()
    {
        string[] lines = this.ReadAllLines();
        List<Game> games = ParseLines(lines);

        List<Game> validGames = games.Where(game => game.HandGrabs.All(hg => hg.Red <= 12 && hg.Green <= 13 && hg.Blue <= 14)).ToList();

        int sum = validGames.Sum(game => game.Id);

        return sum;
    }

    internal override int Solve2()
    {
        string[] lines = this.ReadAllLines();
        List<Game> games = ParseLines(lines);

        int sum = 0;
        foreach (Game game in games)
        {
            int minRed = game.HandGrabs.Max(hg => hg.Red);
            int minGreen = game.HandGrabs.Max(hg => hg.Green);
            int minBlue = game.HandGrabs.Max(hg => hg.Blue);

            int power = minRed * minGreen * minBlue;
            sum += power;
        }

        return sum;
    }

    internal List<Game> ParseLines(string[] lines)
    {
        List<Game> games = new List<Game>();
        // parse lines of the form "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue"
        foreach (string line in lines)
        {
            Game game = new Game();
            string[] splitColon = line.Split(": ");
            int gameId = int.Parse(splitColon[0].Split(' ')[1]);
            game.Id = gameId;
            foreach (string handGrabString in splitColon[1].Split("; "))
            {
                HandGrab handGrab = new HandGrab();
                string[] splitComma = handGrabString.Split(", ");
                foreach (string grab in splitComma)
                {
                    string[] splitSpace = grab.Split(' ');
                    int amount = int.Parse(splitSpace[0]);
                    string color = splitSpace[1];
                    switch (color)
                    {
                        case "blue":
                            handGrab.Blue = amount;
                            break;
                        case "red":
                            handGrab.Red = amount;
                            break;
                        case "green":
                            handGrab.Green = amount;
                            break;
                        default:
                            throw new Exception("Unknown color");
                    }
                }
                game.HandGrabs.Add(handGrab);
            }
            games.Add(game);
        }

        return games;
    }
}

public class Game
{
    public int Id { get; set; }
    public List<HandGrab> HandGrabs = new List<HandGrab>();
}


public class HandGrab
{
    public int Blue { get; set; }
    public int Red { get; set; }
    public int Green { get; set; }
}