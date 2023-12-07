using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Solutions.Utils;

internal abstract class SolutionBase
{
    protected List<(int, int)> adjacentNeighbors = new List<(int, int)>()
    {
        (-1, -1),
        (-1, 0),
        (-1, 1),
        (0, -1),
        (0, 1),
        (1, -1),
        (1, 0),
        (1, 1)
    };

    internal abstract int Solve1();
    internal abstract int Solve2();

    internal string GetInputFilePath(string callerPath, string fileName)
    {
        string inputFilePath = $"{Path.GetDirectoryName(callerPath)!}\\{fileName}";

        return inputFilePath;
    }

    internal string[] ReadAllLines(string fileName = "input.txt", [CallerFilePath] string callerPath = "")
    {
        string path = this.GetInputFilePath(callerPath, fileName);

        return File.ReadAllLines(path);
    }

    internal void Print2DArray<T>(T[,] array)
    {
        StringBuilder sb = new StringBuilder();
        for (int y = 0; y < array.GetLength(0); y++)
        {
            for (int x = 0; x < array.GetLength(1); x++)
            {
                sb.Append(array[y, x]);
                sb.Append(", ");
            }
            sb.Append('\n');
        }
        Console.WriteLine(sb.ToString());
    }
}
