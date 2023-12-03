using Solutions.Utils;
using System.Diagnostics;
using System.Xml;

namespace Solutions.Days.Day_01;

internal class Solution : SolutionBase
{
    internal override int Solve1()
    {
        string[] lines = this.ReadAllLines();

        int sum = 0;
        foreach (string line in lines)
        {
            int first = line.First(c => Char.IsDigit(c)) - '0';
            int last = line.Last(c => Char.IsDigit(c)) - '0';
            sum += 10 * first + last;
        }

        return sum;
    }

    internal override int Solve2()
    {
        string[] lines = this.ReadAllLines();

        List<(string, int)> stringToInt = new()
        {
            ("one", 1),
            ("two", 2),
            ("three", 3),
            ("four", 4),
            ("five", 5),
            ("six", 6),
            ("seven", 7),
            ("eight", 8),
            ("nine", 9)
        };

        int sum = 0;
        foreach (string line in lines)
        {
            int first = -1;
            for (int i = 0; i < line.Length; i++)
            {
                if (char.IsDigit(line[i]))
                {
                    first = line[i] - '0';
                    break;
                }
                foreach ((string word, int value) in stringToInt)
                {
                    if (i + word.Length >= line.Length) continue;

                    if (line.Substring(i, word.Length) == word)
                    {
                        first = value;
                    }
                }
                if (first != -1) break;
            }


            int last = -1;
            for (int i = line.Length - 1; i >= 0; i--)
            {
                if (char.IsDigit(line[i]))
                {
                    last = line[i] - '0';
                    break;
                }
                foreach ((string word, int value) in stringToInt)
                {
                    if (i - word.Length < 0) continue;

                    if (line.Substring(i - word.Length + 1, word.Length) == word)
                    {
                        last = value;
                    }
                }
                if (last != -1) break;
            }


            sum += 10 * first + last;
        }

        return sum;
    }
}
