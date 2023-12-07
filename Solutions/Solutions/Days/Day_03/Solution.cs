using Solutions.Utils;

namespace Solutions.Days.Day_03;

internal class Solution : SolutionBase
{
    internal override int Solve1()
    {
        string[] map = this.ReadAllLines(fileName: "input.txt");

        bool[,] adjacentToSymbol = new bool[map.Length, map[0].Length];

        for (int y = 0; y < map.Length; y++)
        {
            for (int x = 0; x < map[y].Length; x++)
            {
                foreach ((int dy, int dx) in this.adjacentNeighbors)
                {
                    int ny = y + dy;
                    int nx = x + dx;
                    if (ny >= 0 && ny < map.Length && nx >= 0 && nx < map[0].Length)
                    {
                        if (!Char.IsDigit(map[ny][nx]) && map[ny][nx] != '.')
                        {
                            adjacentToSymbol[y, x] = true;
                            break;
                        }
                    }
                }
            }
        }

        int sum = 0;
        string currentNumber = "";
        bool isAdjacentToSymbol = false;
        for (int y = 0; y < map.Length; y++)
        {
            for (int x = 0; x < map[y].Length; x++)
            {
                if (Char.IsDigit(map[y][x]))
                {
                    currentNumber += map[y][x];
                    if (adjacentToSymbol[y, x])
                    {
                        isAdjacentToSymbol = true;
                    }
                }
                else
                {
                    if (isAdjacentToSymbol)
                    {
                        sum += int.Parse(currentNumber);
                    }
                    currentNumber = "";
                    isAdjacentToSymbol = false;
                }
            }
        }


        return sum;
    }

    internal override int Solve2()
    {
        string[] map = this.ReadAllLines(fileName: "input.txt");
        
        Dictionary<(int, int), List<int>> gearsWithTheirNumbers = new();

        string currentNumber = "";
        HashSet<(int, int)> currentAdjacentGears = new();
        for (int y = 0; y < map.Length; y++)
        {
            for (int x = 0; x < map[y].Length; x++)
            {
                if (Char.IsDigit(map[y][x]))
                {
                    currentNumber += map[y][x];
                    foreach ((int dy, int dx) in this.adjacentNeighbors)
                    {
                        int ny = y + dy;
                        int nx = x + dx;
                        if (ny >= 0 && ny < map.Length && nx >= 0 && nx < map[0].Length)
                        {
                            if (map[ny][nx] == '*')
                            {
                                currentAdjacentGears.Add((ny, nx));
                            }
                        }
                    }
                }
                else
                {
                    if (currentAdjacentGears.Count > 0)
                    {
                        int number = int.Parse(currentNumber);
                        foreach ((int, int) gear in currentAdjacentGears)
                        {
                            if (gearsWithTheirNumbers.ContainsKey(gear))
                            {
                                gearsWithTheirNumbers[gear].Add(number);
                            }
                            else
                            {
                                gearsWithTheirNumbers[gear] = new() { number};
                            }
                        }
                    }
                    currentNumber = "";
                    currentAdjacentGears.Clear();
                }
            }
        }

        int sum = gearsWithTheirNumbers
            .Where(kvp => kvp.Value.Count == 2)
            .Select(kvp => kvp.Value[0] * kvp.Value[1])
            .Sum();

        return sum;
    }
}
