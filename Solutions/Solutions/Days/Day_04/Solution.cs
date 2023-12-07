using Solutions.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutions.Days.Day_04;
internal class Solution : SolutionBase
{
    internal override int Solve1()
    {
        string[] lines = this.ReadAllLines(fileName: "input.txt");
        int sum = 0;
        foreach (string line in lines)
        {
            string values = line.Split(":")[1];
            HashSet<int> winning = values.Split("|")[0].Trim().Replace("  ", " ").Split(" ").Select(int.Parse).ToHashSet();
            HashSet<int> my = values.Split("|")[1].Trim().Replace("  ", " ").Split(" ").Select(int.Parse).ToHashSet();

            int count = my.Intersect(winning).Count();
            if (count == 0)
            {
                continue;
            }
            sum += (int) Math.Pow(2, count - 1);
        }
        return sum;
    }

    internal override int Solve2()
    {
        string[] lines = this.ReadAllLines(fileName: "input.txt");
        int[] copies = new int[lines.Length];
        for (int i = 0; i < lines.Length; i++)
        {
            copies[i] = 1;
        }
        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            string values = line.Split(":")[1];
            HashSet<int> winning = values.Split("|")[0].Trim().Replace("  ", " ").Split(" ").Select(int.Parse).ToHashSet();
            HashSet<int> my = values.Split("|")[1].Trim().Replace("  ", " ").Split(" ").Select(int.Parse).ToHashSet();

            int count = my.Intersect(winning).Count();
            for (int j = 1; j < count + 1; j++)
            {
                if (i + j >= copies.Length)
                {
                    break;
                }
                copies[i + j] += copies[i];
            }
        }

        Console.WriteLine(string.Join(", ", copies));
        return copies.Sum();
    }
}
