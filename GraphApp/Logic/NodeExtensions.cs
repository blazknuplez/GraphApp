using System.Collections.Generic;

namespace GraphApp
{
    public static class NodeExtensions
    {
        public static void InsertConnection(this Node nodeFrom, string connectionName, Node nodeTo)
        {
            if (!nodeFrom.Connections.TryGetValue(connectionName, out List<Node> connectionList))
            {
                connectionList = new List<Node>();
                nodeFrom.Connections.Add(connectionName, connectionList);
            }

            connectionList.Add(nodeTo);
        }
    }
}
