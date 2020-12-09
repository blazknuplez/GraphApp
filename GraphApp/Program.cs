using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Enumeration;
using System.Linq;
using System.Text;

namespace GraphApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = @".\Data\avtomat-todo.aut";
            List<string> connections = new List<string> { "F0!", "StartTX!", "G0!", "ACK!", "B!", "StopTX!", "SOK!", "REQ?", "F0!" };

            INodeParser parser = new NodeParser();
            var nodes = parser.ParseData(fileName);

            Console.WriteLine($"Number of nodes: {nodes.Count}");
            Console.WriteLine($"Number of connections: {ConnectionCount(nodes)}");
            Console.WriteLine($"Searching for: {string.Join(',', connections)}");

            IConnectionAnalyser connectionAnalyser = new ConnectionAnalyser();
            var solutions = connectionAnalyser.FindConnections(nodes, connections);

            Console.WriteLine();
            Console.WriteLine($"Solutions found: {solutions.Count}");

            int solutionIndex = 0;
            foreach (var solution in solutions)
            {
                Console.WriteLine(FormatRow(++solutionIndex, solution, connections));
            }
        }

        private static int ConnectionCount(Dictionary<int, Node> nodes) 
            => nodes.Values.ToList().SelectMany(i => i.Connections.Values.ToList()).Sum(i => i.Count);

        private static string FormatRow(int solutionIndex, List<Node> solution, List<string> connections)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Solution {solutionIndex}: ");

            for (int i = 0; i < connections.Count; i++)
                sb.Append($"{solution[i].NodeId}, {connections[i]}, ");

            sb.Append(solution.LastOrDefault()?.NodeId);

            return sb.ToString();
        }
    }
}
