using System.Collections.Generic;

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

        public override string ToString()
        {
            return $"{nameof(NodeId)}: {NodeId}";
        }
    }
}
