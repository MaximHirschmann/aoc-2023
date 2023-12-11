using Solutions.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutions.Days.Day_09;
internal class Solution : SolutionBase
{
    internal override int Solve1()
    {
        string[] lines = this.ReadAllLines("input.txt");

        long sum = 0;
        foreach (string line in lines)
        {
            long[] nums = line.Split(" ").Select(long.Parse).ToArray();

            sum += this.SolveSequence(nums);
        }
        return (int) sum;
    }

    internal override int Solve2()
    {
        string[] lines = this.ReadAllLines("input.txt");

        long sum = 0;
        foreach (string line in lines)
        {
            long[] nums = line.Split(" ").Select(long.Parse).ToArray();

            nums = nums.Reverse().ToArray();

            sum += this.SolveSequence(nums);
        }

        return (int) sum;
    }

    private long SolveSequence(long[] nums)
    {
        long pred = nums[^1];
        long[] prev = nums.ToArray();
        while (prev.Any(x => x != 0))
        {
            long[] next = new long[prev.Length - 1];
            for (int i = 1; i < prev.Length; i++)
            {
                next[i - 1] = prev[i] - prev[i - 1];
            }
            pred += next[^1];
            prev = next.ToArray();
        }

        return pred;
    }
}

