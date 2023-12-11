using Solutions.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Solutions.Days.Day_08;
internal class Solution : SolutionBase
{
    internal override int Solve1()
    {
        string[] lines = this.ReadAllLines("input.txt");

        Graph graph = new();
        for (int i = 2; i < lines.Length; i++)
        {
            string line = lines[i];
            string[] parts = line.Split(" = ");
            string name = parts[0];
            string left = parts[1][1..4];
            string right = parts[1][6..9];

            graph.AddNode(name, left, right);
        }


        string lr = lines[0];
        graph.CurrentNode = "AAA";
        int count = 0;
        while (graph.CurrentNode != "ZZZ")
        {
            if (lr[count % lr.Length] == 'L')
            {
                graph.MoveLeft();
            }
            else if (lr[count % lr.Length] == 'R')
            {
                graph.MoveRight();
            }
            count++;
        }


        return count;
    }

    internal override int Solve2()
    {
        string[] lines = this.ReadAllLines("input.txt");

        List<string> endsWithA = new();

        Graph graph = new();
        for (int i = 2; i < lines.Length; i++)
        {
            string line = lines[i];
            string[] parts = line.Split(" = ");
            string name = parts[0];
            string left = parts[1][1..4];
            string right = parts[1][6..9];

            if (name.EndsWith('A'))
            {
                endsWithA.Add(name);
            }
            graph.AddNode(name, left, right);
        }

        string lr = lines[0];
        List<long> cycleLengths = new();
        for (int i = 0; i < endsWithA.Count; i++)
        {
            graph.CurrentNode = endsWithA[i];

            int count = 0;
            while (true)
            {
                if (lr[count % lr.Length] == 'L')
                {
                    graph.MoveLeft();
                }
                else if (lr[count % lr.Length] == 'R')
                {
                    graph.MoveRight();
                }
                count++;
                if (graph.CurrentNode.EndsWith('Z'))
                {
                    cycleLengths.Add(count);
                    break;
                }
            }
        }

        BigInteger leastCommonMultiple = cycleLengths.Aggregate(new BigInteger(1), (agg, val) => agg = agg * val / BigInteger.GreatestCommonDivisor(agg, val));
        Console.WriteLine(leastCommonMultiple);

        return -1;
    }
}

class Graph
{
    public string CurrentNode { get; set; }

    private Dictionary<string, (string, string)> Nodes { get; set; }

    public Graph()
    {
        this.Nodes = new();
    }

    public void AddNode(string name, string left, string right)
    {
        this.Nodes.Add(name, (left, right));
    }

    public void MoveLeft()
    {
        CurrentNode = this.Nodes[CurrentNode].Item1;
    }
    public void MoveRight()
    {
        CurrentNode = this.Nodes[CurrentNode].Item2;
    }
}
