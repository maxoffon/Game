using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp8
{
	public static class NodeExtensions
	{
		public static IEnumerable<Node> BreadthSearch(this Node startNode)
		{
			var visited = new HashSet<Node>();
			var queue = new Queue<Node>();
			queue.Enqueue(startNode);
			while (queue.Count != 0)
			{
				var node = queue.Dequeue();
				if (visited.Contains(node)) continue;
				visited.Add(node);
				yield return node;
				foreach (var incidentNode in node.IncidentNodes)
					queue.Enqueue(incidentNode);
			}
		}
	}
	public class Node
	{
		readonly List<Edge> edges = new List<Edge>();
		public readonly int NodeNumber;

		public Node(int number)
		{
			NodeNumber = number;
		}

		public IEnumerable<Node> IncidentNodes
		{
			get
			{
				return edges.Select(z => z.OtherNode(this));
			}
		}
		public IEnumerable<Edge> IncidentEdges
		{
			get
			{
				foreach (var e in edges) yield return e;
			}
		}
		public static Edge Connect(Node node1, Node node2, int value, Graph graph)
		{
			if (!graph.Nodes.Contains(node1) || !graph.Nodes.Contains(node2)) throw new ArgumentException();
			var edge = new Edge(node1, node2, value);
			node1.edges.Add(edge);
			node2.edges.Add(edge);
			return edge;
		}
	}
}
