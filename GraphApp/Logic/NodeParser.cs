using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GraphApp
{
    public interface INodeParser
    {
        Dictionary<int, Node> ParseData(string filePath);
    }

    public class NodeParser : INodeParser
    {
        public Dictionary<int, Node> ParseData(string filePath)
        {
            Dictionary<int, Node> nodes = new Dictionary<int, Node>();
            StreamReader file = new StreamReader(filePath);

            string line;
            while ((line = file.ReadLine()) != null)
            {
                var items = line.Split(',');

                int nodeFromId = int.Parse(items[0]);
                string connection = items[1];
                int nodeToId = int.Parse(items[2]);

                Node nodeFrom = GetOrAddNode(nodes, nodeFromId);
                Node nodeTo = GetOrAddNode(nodes, nodeToId);

                nodeFrom.InsertConnection(connection, nodeTo);
            }

            return nodes;
        }

        private Node GetOrAddNode(Dictionary<int, Node> nodes, int nodeId)
        {
            Node node = nodes.GetValueOrDefault(nodeId);

            if (node == null)
            {
                node = new Node(nodeId);
                nodes.Add(node.NodeId, node);
            }

            return node;
        }
    }
}
