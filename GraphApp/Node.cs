using System;
using System.Collections.Generic;
using System.Text;

namespace GraphApp
{
    public class Node
    {
        public Node(int nodeId)
        {
            NodeId = nodeId;
        }

        public int NodeId { get; set; }

        public Dictionary<string, List<Node>> Connections { get; set; } = new Dictionary<string, List<Node>>();

        public void InsertConnection(string connectionName, Node node)
        {
            if (!Connections.TryGetValue(connectionName, out List<Node> connectionList))
            {
                connectionList = new List<Node>();
                Connections.Add(connectionName, connectionList);
            }

            connectionList.Add(node);
        }

        public override string ToString()
        {
            return $"NodeId: {NodeId}";
        }
    }
}
