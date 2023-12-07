using Solutions.Utils;

namespace Solutions.Days.Day_07;
internal class Solution : SolutionBase
{
    internal override int Solve1()
    {
        string[] lines = this.ReadAllLines("input.txt");

        HandComparer cardComparer = new();

        var temp = lines.Select(line => (hand: line[..5], bid: int.Parse(line[6..].Trim()))).OrderBy(item => item.hand, cardComparer).ToList();
        var temp2 = lines.Select(line => (hand: line[..5], bid: int.Parse(line[6..].Trim()))).Select(item => (item.hand, cardComparer.CardValue(item.hand))).ToList();
        return lines.Select(line => (hand: line[..5], bid: int.Parse(line[6..].Trim()))).OrderBy(item => item.hand, cardComparer).Select((item, ind) => (ind + 1) * item.bid).Sum();
    }

    internal override int Solve2()
    {
        string[] lines = this.ReadAllLines("input.txt");

        JokerHandComparer cardComparer = new();

        var temp = lines.Select(line => (hand: line[..5], bid: int.Parse(line[6..].Trim()))).OrderBy(item => item.hand, cardComparer).ToList();
        var temp2 = lines.Select(line => (hand: line[..5], bid: int.Parse(line[6..].Trim()))).Select(item => (item.hand, cardComparer.JokerHandValue(item.hand))).ToList();
        return lines.Select(line => (hand: line[..5], bid: int.Parse(line[6..].Trim()))).OrderBy(item => item.hand, cardComparer).Select((item, ind) => (ind + 1) * item.bid).Sum();
    }
}


internal class HandComparer : IComparer<string>
{
    private readonly Dictionary<char, int> charValues = new()
    {
        { '2', 2 },
        { '3', 3 },
        { '4', 4 },
        { '5', 5 },
        { '6', 6 },
        { '7', 7 },
        { '8', 8 },
        { '9', 9 },
        { 'T', 10 },
        { 'J', 11},
        { 'Q', 12 },
        { 'K', 13 },
        { 'A', 14 }
    };
    public int Compare(string? x, string? y)
    {
        if (x == null || y == null)
        {
            throw new ArgumentOutOfRangeException();
        }

        var xValue = CardValue(x);
        var yValue = CardValue(y);

        if (xValue != yValue)
        {
            return xValue.CompareTo(yValue);
        }

        for (var i = 0; i < x.Length; i++)
        {
            int xV = this.charValues[x[i]];
            int yV = this.charValues[y[i]];
            if (xV != yV)
            {
                return xV.CompareTo(yV);
            }
        }

        return 0;
    }

    public int CardValue(string card)
    {
        // five of a kind
        if (card.Distinct().Count() == 1)
        {
            return 6;
        }
        // four of a kind
        if (card.GroupBy(c => c).Any(g => g.Count() == 4))
        {
            return 5;
        }
        // full house
        if (card.Distinct().Count() == 2 && card.GroupBy(c => c).Any(g => g.Count() == 3))
        {
            return 4;
        }
        // three of a kind
        if (card.GroupBy(c => c).Any(g => g.Count() == 3))
        {
            return 3;
        }
        // two pair
        if (card.GroupBy(c => c).Count(g => g.Count() == 2) == 2)
        {
            return 2;
        }
        // one pair
        if (card.GroupBy(c => c).Any(g => g.Count() == 2))
        {
            return 1;
        }
        // high card
        return 0;
    }
}

public class JokerHandComparer : IComparer<string>
{
    private readonly Dictionary<char, int> charValues = new()
    {
        { 'J', 1},
        { '2', 2 },
        { '3', 3 },
        { '4', 4 },
        { '5', 5 },
        { '6', 6 },
        { '7', 7 },
        { '8', 8 },
        { '9', 9 },
        { 'T', 10 },
        { 'Q', 12 },
        { 'K', 13 },
        { 'A', 14 }
    };

    public int Compare(string? x, string? y)
    {
        if (x == null || y == null)
        {
            throw new ArgumentOutOfRangeException();
        }

        var xValue = JokerHandValue(x);
        var yValue = JokerHandValue(y);

        if (xValue != yValue)
        {
            return xValue.CompareTo(yValue);
        }

        for (var i = 0; i < x.Length; i++)
        {
            int xV = this.charValues[x[i]];
            int yV = this.charValues[y[i]];
            if (xV != yV)
            {
                return xV.CompareTo(yV);
            }
        }

        return 0;
    }

    public int JokerHandValue(string hand)
    {
        int max = 0;
        foreach (char newVal in this.charValues.Keys)
        {
            string temp = hand.Replace('J', newVal);
            int tempVal = CardValue(temp);
            if (tempVal > max)
            {
                max = tempVal;
            }
        }
        return max;
    }

    public int CardValue(string card)
    {
        // five of a kind
        if (card.Distinct().Count() == 1)
        {
            return 6;
        }
        // four of a kind
        if (card.GroupBy(c => c).Any(g => g.Count() == 4))
        {
            return 5;
        }
        // full house
        if (card.Distinct().Count() == 2 && card.GroupBy(c => c).Any(g => g.Count() == 3))
        {
            return 4;
        }
        // three of a kind
        if (card.GroupBy(c => c).Any(g => g.Count() == 3))
        {
            return 3;
        }
        // two pair
        if (card.GroupBy(c => c).Count(g => g.Count() == 2) == 2)
        {
            return 2;
        }
        // one pair
        if (card.GroupBy(c => c).Any(g => g.Count() == 2))
        {
            return 1;
        }
        // high card
        return 0;
    }
}