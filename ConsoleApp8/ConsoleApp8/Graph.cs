using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp8
{
	public class Graph
	{
		public Node[] nodes;

		public Graph(int nodesCount)
		{
			nodes = Enumerable.Range(0, nodesCount).Select(z => new Node(z)).ToArray();
		}

		public int Length { get { return nodes.Length; } }

		public Node this[int index] { get { return nodes[index]; } }

		public IEnumerable<Node> Nodes
		{
			get
			{
				foreach (var node in nodes) yield return node;
			}
		}

		public void Connect(int index1, int index2, int value)
		{
			Node.Connect(nodes[index1], nodes[index2], value, this);
		}

		public void Delete(Edge edge)
		{
			Node.Disconnect(edge);
		}

		public IEnumerable<Edge> Edges
		{
			get
			{
				return nodes.SelectMany(z => z.IncidentEdges).Distinct();
			}
		}

		public static Graph MakeGraph(params int[] incidentNodes)
		{
			var graph = new Graph(incidentNodes.Max() + 1);
			for (int i = 0; i < incidentNodes.Length - 1; i += 2)
				graph.Connect(incidentNodes[i], incidentNodes[i + 1], 0);
			return graph;
		}
	}
}
