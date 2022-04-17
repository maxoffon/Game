using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp8
{
	public class Edge
	{
		public readonly Node From;
		public readonly Node To;
		public readonly int Value;
		public Edge(Node first, Node second, int value)
		{
			this.From = first;
			this.To = second;
			this.Value = value;
		}
		public bool IsIncident(Node node)
		{
			return From == node || To == node;
		}
		public Node OtherNode(Node node)
		{
			if (!IsIncident(node)) throw new ArgumentException();
			if (From == node) return To;
			return From;
		}
	}
}
