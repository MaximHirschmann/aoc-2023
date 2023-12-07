using Solutions.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutions.Days.Day_06;
internal class Solution : SolutionBase
{
    private readonly int[] times = { 59, 68, 82, 74 };
    private readonly int[] distances = { 543, 1020, 1664, 1022 };
    //private readonly int[] times = { 7, 15, 30 };
    //private readonly int[] distances = { 9, 40, 200 };

    private readonly long totalTime = 59688274;
    private readonly long totalDistance = 543102016641022;

    internal override int Solve1()
    {
        return times.Zip(distances).Aggregate(1, (acc, td) => acc * this.Count(td.First, td.Second));
    }

    internal override int Solve2()
    {
        return this.Count(this.totalTime, this.totalDistance);
    }

    private int Count(long time, long distance)
    {
        double lowerBound = (time / 2.0) - Math.Sqrt((time / 2.0) * (time / 2.0) - distance);
        double upperBound = (time / 2.0) + Math.Sqrt((time / 2.0) * (time / 2.0) - distance);

        int lower = (int)Math.Ceiling(lowerBound + 0.00001);
        int upper = (int)Math.Floor(upperBound - 0.00001);

        return upper - lower + 1;
    }   
}
