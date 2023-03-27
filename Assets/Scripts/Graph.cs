using System.Collections.Generic;
using System.Linq;

public class Metro
{
    public Dictionary<string, List<string>> StationsAndConnections = new();
    public Dictionary<(string, string), string> Branches = new();
    public void AddConnection(string from, string to, string branch)
    {
        if (!StationsAndConnections.ContainsKey(from))
            AddStation(from);
        if (!StationsAndConnections.ContainsKey(to))
            AddStation(to);
        StationsAndConnections[from].Add(to);
        StationsAndConnections[to].Add(from);
        (string, string) t = (from, to);
        if (!(Branches.ContainsKey(t) || Branches.ContainsKey(Reverse(t))))
        {
            Branches.Add(t, branch);
        }
    }
    public void AddStation(string name)
    {
        if (!StationsAndConnections.ContainsKey(name))
            StationsAndConnections.Add(name, new());
    }
    public (List<string>, List<string>)GetShortestDistance(string start, string goal)
    {
        Dictionary<string, string> visited = new();
        Queue<string> queue = new();
        visited.Add(start, null);
        queue.Enqueue(start);

        while(queue.Count > 0)
        {
            string cur = queue.Dequeue();
            if(cur == goal)
                break;
            foreach(var next in StationsAndConnections[cur])
            {
                if(!visited.ContainsKey(next))
                {
                    queue.Enqueue(next);
                    visited[next] = cur;
                }
            }
        }
        List<string> result = new() { goal };
        string cur_node = goal;
        while (cur_node != start)
        {
            cur_node = visited[cur_node];
            result.Add(cur_node);
        }
        result.Reverse();
        return (result, GetTransfersCount(result));
    }
    public List<string> GetTransfersCount(List<string> list)
    {
        var result = new List<string>();
        if (list.Count == 0)
            return null;
        if (Branches.ContainsKey((list[0], list[1])))
        {
            result.Add(Branches[(list[0], list[1])]);
        }
        else if (Branches.ContainsKey((list[1], list[0])))
        {
            result.Add(Branches[(list[1], list[0])]);
        }
        for(int i = 1; i < list.Count - 1; i ++)
        {
            if (Branches.ContainsKey((list[i], list[i + 1])))
            {
                if(Branches[(list[i], list[i + 1])] != result.Last())
                    result.Add(Branches[(list[i], list[i + 1])]);
            }
            else if (Branches.ContainsKey((list[1], list[0])))
            {
                if (Branches[(list[i + 1], list[i])] != result.Last())
                    result.Add(Branches[(list[i + 1], list[i])]);
            }
        }
        return result;
    }
    private (string, string) Reverse((string, string) tuple)
    {
        return (tuple.Item2, tuple.Item1);
    }
}