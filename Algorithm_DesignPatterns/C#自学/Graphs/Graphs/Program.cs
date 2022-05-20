using System;
using System.Collections;
using System.Collections.Generic;

namespace Graphs
{
	public class Graph
	{
		public Hashtable nodes;
		public HashSet<Edge> edges;
		public Graph() {
			nodes = new Hashtable();
			edges = new HashSet<Edge>();
		}
	}
	public class Node
	{
		public int value;//图中节点的值
		public int inDegree;//节点的入度
		public int outDegree;//节点的出度
		public List<Node> nexts;//有向图中节点x指向的所有结点
		public List<Edge> edges;//有向图中结点x所发散的边
		public Node(int value) {
			this.value = value;
			inDegree = 0;
			outDegree = 0;
			nexts = new List<Node>();
			edges = new List<Edge>();
		}
	}
	public class Edge
	{
		public int weight;//边的权值
		public Node from;
		public Node to;
		public Edge(int weight, Node from, Node to) {
			this.weight = weight;
			this.from = from;
			this.to = to;
		}
	}

	//一个将题目中的图结构转化为上述自己熟悉的结构的例子
	public class ConvertToGraph
	{
		public static Graph CreateGraph(int[][] matrix) {
			Graph graph = new Graph();
			for (int i = 0; i < matrix.Length; i++) {
				int from = matrix[i][0];
				int to = matrix[i][1];
				int weight = matrix[i][2];
				if (!graph.nodes.ContainsKey(from)) {
					graph.nodes.Add(from, new Node(from));
				}
				if (!graph.nodes.ContainsKey(to)) {
					graph.nodes.Add(to, new Node(from));
				}
				Node fromNode = (Node)graph.nodes[from];
				Node toNode = (Node)graph.nodes[to];
				Edge newEdge = new Edge(weight, fromNode, toNode);
				fromNode.nexts.Add(toNode);
				fromNode.outDegree++;
				toNode.inDegree++;
				fromNode.edges.Add(newEdge);
				graph.edges.Add(newEdge);
			}
			return graph;
		}
	}

	//从node出发，进行宽度优先遍历
	public class Code01_BFS
	{
		public static void BFS(Node node) {
			if (node == null) {
				return;
			}
			Queue<Node> queue = new Queue<Node>();
			HashSet<Node> set = new HashSet<Node>();
			queue.Enqueue(node);
			set.Add(node);
			while (queue.Count != 0) {
				Node cur = queue.Dequeue();
				Console.WriteLine(cur.value);
				foreach (var next in cur.nexts) {
					if (!set.Contains(next)) {
						set.Add(next);
						queue.Enqueue(next);
					}
				}
			}
		}
	}

	//从node出发，进行深度优先遍历
	public class Code02_DFS
	{
		public static void dfs(Node node) {
			if (node == null) {
				return;
			}
			Stack<Node> stack = new Stack<Node>();
			HashSet<Node> set = new HashSet<Node>();
			stack.Push(node);
			set.Add(node);
			Console.WriteLine(node.value);
			while (stack.Count != 0) {
				Node cur = stack.Pop();
				foreach (var next in cur.nexts) {
					if (!set.Contains(next)) {
						stack.Push(cur);
						stack.Push(next);
						set.Add(next);
						Console.WriteLine(next.value);
						break;
					}
				}
			}
		}
	}

	//拓扑排序
	public class Code03_Topology
	{
		public static List<Node> SortedToplogy(Graph graph) {
			Hashtable inMap = new Hashtable();
			//入度为0的点，进入此队列
			Queue<Node> zeroInQueue = new Queue<Node>();
			foreach (Node node in graph.nodes.Values) {
				inMap.Add(node, node.inDegree);
				if (node.inDegree == 0) {
					zeroInQueue.Enqueue(node);
				}
			}
			//拓扑排序的结果，依次加入result
			List<Node> result = new List<Node>();
			while (zeroInQueue.Count != 0) {
				Node cur = zeroInQueue.Dequeue();
				result.Add(cur);
				foreach (Node next in cur.nexts) {
					inMap.Add(next, (int)inMap[next] - 1);
					if ((int)inMap[next] == 0) {
						zeroInQueue.Enqueue(next);
					}
				}
			}
			return result;
		}
	}

	//kruskal算法
	public class Code04_Kruskal
	{
		//方法一 p7 1:37:23
		public class MySets{
			public Hashtable setMap;
			public MySets(List<Node> nodes) {
				foreach (Node cur in nodes) {
					List<Node> set = new List<Node>();
					set.Add(cur);
					setMap.Add(cur, set);
				}
			}
			public bool IsSameSet(Node from, Node to) {
				List<Node> fromSet = (List<Node>)setMap[from];
				List<Node> toSet = (List<Node>)setMap[to];
				return fromSet == toSet;
			}
			public void Union(Node from, Node to) {
				List<Node> fromSet = (List<Node>)setMap[from];
				List<Node> toSet = (List<Node>)setMap[to];
				foreach (Node toNode in toSet) {
					fromSet.Add(toNode);
					setMap.Add(toNode, fromSet);
				}
			}
		}
		//public static SortedSet<Edge> KruskalMST(Graph graph) {
		//	MySets mySets = new MySets();
		//}
		//方法二并查集结构
	}
	class Program
	{
		static void Main(string[] args) {
			A a = new A();
			a.hashtable.Add("k", new Double());
			a.hashtable.Add(1, 3);
		}
	}
	public class A
	{
		public Hashtable hashtable = new Hashtable();
	}

}
