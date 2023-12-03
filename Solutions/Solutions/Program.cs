using Solutions.Utils;
using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        // -1 to run the latest solution
        int dayToRun = -1;

        Assembly assembly = Assembly.GetExecutingAssembly();

        Type type;
        Type baseType = typeof(SolutionBase);
        if (dayToRun == -1)
        {
            dayToRun = assembly
                .GetTypes()
                .Where(t => baseType.IsAssignableFrom(t) && t != typeof(SolutionBase))
                .Max(t => ExtractDayNumber(t.FullName!));
        }

        type = assembly
            .GetTypes()
            .Where(t => baseType.IsAssignableFrom(t) && t != typeof(SolutionBase))
            .First(t => dayToRun == ExtractDayNumber(t.FullName!));

    
        SolutionBase instance = (SolutionBase)Activator.CreateInstance(type)!;

        Console.WriteLine("***** SOLUTION 1 *******");
        Stopwatch sw = Stopwatch.StartNew();
        Console.WriteLine(instance.Solve1());
        Console.WriteLine(sw.Elapsed);
        Console.WriteLine();

        Console.WriteLine("***** SOLUTION 2 *******");
        sw.Restart();
        Console.WriteLine(instance.Solve2());
        Console.WriteLine(sw.Elapsed);
    }

    // input in the form of e.g.:  "Solutions.Days.Day_01.Solution"
    static int ExtractDayNumber(string input)
    {
        string pattern = @"Day_(\d+)";

        // Use Regex.Match to find the first match in the input string
        Match match = Regex.Match(input, pattern);

        // Check if a match was found
        if (match.Success)
        {
            // Extract the captured group value and parse it as an integer
            if (int.TryParse(match.Groups[1].Value, out int dayNumber))
            {
                return dayNumber;
            }
        }

        return -1;
    }
}
