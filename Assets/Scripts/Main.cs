using UnityEngine;
using Newtonsoft.Json;

class Main : MonoBehaviour
{
    public Metro metro = new();
    private void Start()
    {
        metro.StationsAndConnections = new()
        {
            { "A", new() { "B" } },
            { "B", new() { "A", "C" } },
            { "C", new() { "B", "J", "D", "K" } },
            { "D", new() { "C", "J", "E", "L" } },
            { "E", new() { "D", "J", "F", "M" } },
            { "F", new() { "E", "J", "G" } },
            { "G", new() { "F" } },
            { "H", new() { "B", "J" } },
            { "J", new() { "H", "O", "F", "E", "D", "C" } },
            { "K", new() { "C", "L" } },
            { "L", new() { "K", "D", "M", "N" } },
            { "M", new() { "L", "E" } },
            { "N", new() { "L" } },
            { "O", new() { "J" } }
        };
        metro.Branches = new()
        {
            { ("A", "B"), "red" },
            { ("B", "C"), "red" },
            { ("C", "D"), "red" },
            { ("D", "E"), "red" },
            { ("E", "F"), "red" },

            { ("B", "H"), "black" },
            { ("H", "J"), "black" },
            { ("J", "F"), "black" },
            { ("F", "G"), "black" },

            { ("N", "L"), "blue" },
            { ("L", "D"), "blue" },
            { ("D", "J"), "blue" },
            { ("J", "O"), "blue" }, 

            { ("C", "J"), "green" }, 
            { ("J", "E"), "green" } ,
            { ("E", "M"), "green" } ,
            { ("M", "L"), "green" } ,
            { ("L", "K"), "green" } ,
            { ("K", "C"), "green" } 
        };
        var res = metro.GetShortestDistance("A", "G");
        Debug.Log(JsonConvert.SerializeObject(res.Item1));
        Debug.Log($"transfer count: {res.Item2.Count - 1}, {JsonConvert.SerializeObject(res.Item2)}");
    }
}