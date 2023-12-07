using Solutions.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutions.Days.Day_05;
internal class Solution : SolutionBase
{
    internal override int Solve1()
    {
        string[] lines = this.ReadAllLines(fileName: "input.txt");

        List<long> seeds = lines[0].Split(": ")[1].Trim().Split(" ").Select(long.Parse).ToList();
        List<long> newSeeds = new();
        List<bool> taken = new();
        for (int i = 0; i < seeds.Count; i++)
        {
            taken.Add(false);
        }

        for (int i = 2; i < lines.Length; i++)
        {
            string line = lines[i];
            if (line.Contains("map"))
            {
                for (int j = 0; j < seeds.Count; j++)
                {
                    if (!taken[j])
                    {
                        newSeeds.Add(seeds[j]);
                    }
                }
                seeds = newSeeds;
                newSeeds = new();
                taken = new();
                for (int j = 0; j < seeds.Count; j++)
                {
                    taken.Add(false);
                }
            }
            else if (line == "")
            {

            }
            else
            {
                long[] values = line.Trim().Split(" ").Select(long.Parse).ToArray();
                long dest = values[0];
                long source = values[1];
                long range = values[2];


                for (int j = 0; j < seeds.Count; j++)
                {
                    long seed = seeds[j];
                    if (seed >= source && seed < source + range)
                    {
                        newSeeds.Add(dest + seed - source);
                        taken[j] = true;
                    }
                }
            }
        }

        for (int j = 0; j < seeds.Count; j++)
        {
            if (!taken[j])
            {
                newSeeds.Add(seeds[j]);
            }
        }
        seeds = newSeeds;


        return (int) seeds.Min();
    }

    internal override int Solve2()
    {
        string[] lines = this.ReadAllLines(fileName: "input.txt");

        long[] seedSplit = lines[0].Split(": ")[1].Trim().Split(" ").Select(long.Parse).ToArray();
        List<SeedRange> seeds = new();
        for (int i = 0; i < seedSplit.Length; i += 2)
        {
            seeds.Add(new SeedRange(seedSplit[i], seedSplit[i + 1]));
        }
        List<SeedRange> newSeeds = new();
        for (int i = 2; i < lines.Length; i++)
        {
            string line = lines[i];
            if (line.Contains("map"))
            {
                seeds.AddRange(newSeeds);
                newSeeds = new();
            }
            else if (line == "")
            {

            }
            else
            {
                long[] values = line.Trim().Split(" ").Select(long.Parse).ToArray();
                long dest = values[0];
                long source = values[1];
                long range = values[2];


                List<SeedRange> temp = new();
                for (int j = 0; j < seeds.Count; j++)
                {
                    SeedRange seedRange = seeds[j];
                    if (seedRange.Contains(source, range))
                    {
                        (List<SeedRange> originalSeedRanges, SeedRange modified) = seedRange.SplitBy(source, dest, range);
                        newSeeds.Add(modified);
                        temp.AddRange(originalSeedRanges);
                    }
                    else
                    {
                        temp.Add(seedRange);
                    }
                }
                seeds = temp;

            }
        }

        seeds.AddRange(newSeeds);

        return (int)seeds.Select(sr => sr.Start).Min();
    }
}

internal class  SeedRange
{
    internal long Start { get; set; }
    internal long Length { get; set; }

    internal long End => this.Start + this.Length - 1;

    internal SeedRange(long start, long length)
    {
        this.Start = start;
        this.Length = length;
    }

    internal bool Contains(long source, long range)
    {
        long start = source;
        long end = source + range - 1;

        return start <= this.End && end >= this.Start;
    }

    internal (List<SeedRange> original, SeedRange modified) SplitBy(long source, long dest, long range)
    {
        List<SeedRange> oldSeedRanges = new();
        long start = source;
        long end = source + range - 1;

        start = Math.Max(start, this.Start);
        end = Math.Min(end, this.End);

        if (start - this.Start != 0)
        {
            oldSeedRanges.Add(new SeedRange(this.Start, start - this.Start));
        }

        SeedRange modified = new SeedRange(start + dest - source, end - start + 1);

        if (this.End - end != 0)
        {
            oldSeedRanges.Add(new SeedRange(end + 1, this.End - end));
        }

        return (oldSeedRanges, modified);
    }

    
}
