using System;
using System.Collections.Generic;
using System.Linq;
namespace ConsoleApp8
{
	class Program
	{
		public static List<Node> FindPath(Node start, Node end)
		{
			var track = new Dictionary<Node, Node>();
			track[start] = null;
			var queue = new Queue<Node>();
			queue.Enqueue(start);
			while (queue.Count != 0)
			{
				var node = queue.Dequeue();
				var next = node.IncidentEdges.OrderBy(x => x.Value).First().OtherNode(node);
				track[next] = node;
				queue.Enqueue(next);
				if (track.ContainsKey(end)) break;
			}
			var pathItem = end;
			var result = new List<Node>();
			while (pathItem != null)
			{
				result.Add(pathItem);
				pathItem = track[pathItem];
			}
			result.Reverse();
			return result;
		}

		public static void Main()
		{
			var graph = Graph.MakeGraph(
				0, 1,
				1, 2,
				2, 3,
				3, 4,
				0, 3);
			Node.Connect(graph.nodes[0], graph.nodes[3], 1, graph);
			Node.Connect(graph.nodes[3], graph.nodes[4], 1, graph);
			foreach (var elem in FindPath(graph.nodes[0],graph.nodes[4]))
				Console.Write(elem.NodeNumber + " ");
		}
	}
}
