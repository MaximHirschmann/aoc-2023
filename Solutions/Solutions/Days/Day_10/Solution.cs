using Solutions.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutions.Days.Day_10;
internal class Solution : SolutionBase
{
    internal override int Solve1()
    {
        string[] lines = this.ReadAllLines("test.txt");

        int startY = -1;
        int startX = -1;
        int[][] distances = new int[lines.Length][];
        for (int i = 0; i < lines.Length; i++)
        {
            distances[i] = new int[lines[i].Length];
            for (int j = 0; j < lines[i].Length; j++)
            {
                distances[i][j] = -1;
                if (lines[i][j] == 'S')
                {
                    startY = i;
                    startX = j;
                }
            }
        }

        void AddIfConnecting(Point from, Point to, char[] valid, ref List<Point> points)
        {
            char c = lines[to.Y][to.X];
            if (valid.Contains(c))
            {
                points.Add(to);
            }
        }

        Stack<Point> current = new();
        int currentDistance = 0;
        while (current.Count != 0)
        {
            List<Point> candidates = new();
            foreach (Point point in current)
            {
                distances[point.Y][point.X] = currentDistance;

                // Add neighbours to stack
                switch (lines[point.Y][point.X])
                {
                    case '|':
                        AddIfConnecting(point, new Point(point.X, point.Y - 1), ['|', 'F', '7'], ref candidates);
                        AddIfConnecting(point, new Point(point.X, point.Y + 1), ['|', 'L', 'J'], ref candidates);
                        break;
                    case '-':
                        AddIfConnecting(point, new Point(point.X - 1, point.Y), ['-', 'L', 'F'], ref candidates);
                        AddIfConnecting(point, new Point(point.X + 1, point.Y), ['-', 'J', '7'], ref candidates);
                        break;
                    case 'L':
                        AddIfConnecting(point, new Point(point.X + 1, point.Y), ['-', 'J', '7'], ref candidates);
                        AddIfConnecting(point, new Point(point.X + 1, point.Y), ['-', 'J', '7'], ref candidates);

                        candidates.Add(new Point(point.X + 1, point.Y));
                        candidates.Add(new Point(point.X, point.Y - 1));
                        break;
                    case 'J':
                        candidates.Add(new Point(point.X - 1, point.Y));
                        candidates.Add(new Point(point.X, point.Y - 1));
                        break;
                    case '7':
                        candidates.Add(new Point(point.X - 1, point.Y)); 
                        candidates.Add(new Point(point.X, point.Y + 1));
                        break;
                    case 'F':
                        candidates.Add(new Point(point.X + 1, point.Y));
                        candidates.Add(new Point(point.X, point.Y + 1));
                        break;
                    case 'S':
                    default:
                        break;
                }
            }
            current.Clear();
            currentDistance++;

            foreach (Point point in candidates)
            {
                if (point.X < 0 || point.Y < 0 || point.X >= distances[0].Length || point.Y >= distances.Length)
                {
                    continue;
                }
                if (lines[point.Y][point.X] == '.')
                {
                    continue;
                }
                if (distances[point.Y][point.X] != -1)
                {
                    continue;
                }
                current.Push(point);
            }
        }


    }

    internal override int Solve2()
    {
        throw new NotImplementedException();
    }
}


class Point
{
    public int X { get; set; }
    public int Y { get; set; }

    public Point()
    {
        this.X = -1;
        this.Y = -1;
    }

    public Point(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }
}