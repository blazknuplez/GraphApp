using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Enumeration;
using System.Linq;

namespace GraphApp
{
    class Program
    {
        private static Dictionary<int, Node> Nodes = new Dictionary<int, Node>();
        private static List<List<Node>> Solutions = new List<List<Node>>();
        private static string FileName = @".\avtomat-todo.aut";
        //private static string FileName = @".\avtomat-test.aut";

        static void Main(string[] args)
        {
            List<string> commands = new List<string> { "F0!", "StartTX!", "G0!", "ACK!", "B!", "StopTX!", "SOK!", "REQ?", "F0!" };
            //List<string> commands = new List<string> { "TAU", "ABC" };

            GetData();

            Console.WriteLine($"Number of nodes: {NodeCount}");
            Console.WriteLine($"Number of connections: {ConnectionCount}");
            Console.WriteLine($"Searching for: {string.Join(',', commands)}");

            FindConnections(commands);

            Console.WriteLine();
            Console.WriteLine($"Solutions found: {Solutions.Count}");

            int index = 0;
            foreach (var solution in Solutions)
            {
                Console.WriteLine($"Solution {++index}: {string.Join(',', solution.Select(i => i.NodeId))}");
            }
        }

        private static void FindConnections(List<string> commands)
        {
            FindNodesByConnection(Nodes.Values.ToList(), commands, 0, null);
        }

        private static List<Node> FindNodesByConnection(List<Node> nodes, List<string> commands, int iteration, List<Node> solution)
        {
            if (iteration > commands.Count - 1)
            {
                nodes.ForEach(fn =>
                {
                    var subResult = new List<Node>(solution);
                    subResult.Add(fn);
                    Solutions.Add(subResult);
                });

                return nodes;
            }

            string connection = commands[iteration];

            foreach(Node node in nodes.Where(i => i.Connections.ContainsKey(connection)))
            {
                int i = iteration;

                var subResult = solution == null ? new List<Node>() : new List<Node>(solution);
                subResult.Add(node);

                var destinationNodes = node.Connections.GetValueOrDefault(connection);

                if (!destinationNodes.Any())
                    continue;

                FindNodesByConnection(destinationNodes, commands, ++i, subResult);
            }

            return nodes;
        }

        private static void GetData()
        {
            StreamReader file = new StreamReader(FileName);

            string line;
            while ((line = file.ReadLine()) != null)
            {
                var items = line.Split(',');

                int nodeFromId = int.Parse(items[0]);
                string connection = items[1];
                int nodeToId = int.Parse(items[2]);

                Node nodeFrom = GetOrAddNode(nodeFromId);
                Node nodeTo = GetOrAddNode(nodeToId);

                nodeFrom.InsertConnection(connection, nodeTo);
            }
        }

        private static Node GetOrAddNode(int nodeId)
        {
            Node node = Nodes.GetValueOrDefault(nodeId);

            if (node == null)
            {
                node = new Node(nodeId);
                Nodes.Add(node.NodeId, node);
            }

            return node;
        }

        private static int NodeCount => Nodes.Count;

        private static int ConnectionCount => Nodes.Values.ToList().SelectMany(i => i.Connections.Values.ToList()).Sum(i => i.Count);
    }
}
