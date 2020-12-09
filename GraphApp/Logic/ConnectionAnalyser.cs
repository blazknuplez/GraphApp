using System.Collections.Generic;
using System.Linq;

namespace GraphApp
{
    public interface IConnectionAnalyser
    {
        List<List<Node>> FindConnections(Dictionary<int, Node> nodes, List<string> connections);
    }

    public class ConnectionAnalyser : IConnectionAnalyser
    {
        private List<List<Node>> _solutions = new List<List<Node>>();

        public List<List<Node>> FindConnections(Dictionary<int, Node> nodes, List<string> connections)
        {
            _solutions.Clear();
            FindNodesByConnection(nodes.Values.ToList(), connections, 0, null);

            return _solutions;
        }

        private List<Node> FindNodesByConnection(List<Node> nodes, List<string> connections, int iteration, List<Node> solution)
        {
            if (iteration > connections.Count - 1)
            {
                nodes.ForEach(node =>
                {
                    var result = new List<Node>(solution);
                    result.Add(node);
                    _solutions.Add(result);
                });

                return nodes;
            }

            string connection = connections[iteration];

            foreach (Node node in nodes.Where(i => i.Connections.ContainsKey(connection)))
            {
                var partialResult = solution == null ? new List<Node>() : new List<Node>(solution);
                partialResult.Add(node);

                var destinationNodes = node.Connections.GetValueOrDefault(connection);

                if (!destinationNodes.Any())
                    continue;

                FindNodesByConnection(destinationNodes, connections, iteration + 1, partialResult);
            }

            return nodes;
        }
    }
}
