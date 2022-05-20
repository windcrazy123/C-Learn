using System;
using System.Collections;
using System.Collections.Generic;

namespace 二叉树
{
	//二叉树结构
	public class Node
	{
		public int value;
		public Node left;
		public Node right;
		public Node(int value) {
			this.value = value;
		}
	}
	public class Node2
	{
		public int value;
		public Node2 left;
		public Node2 right;
		public Node2 parent;
		public Node2(int value) {
			this.value = value;
		}
	}

	//先，中，后序递归与非递归打印 + 介绍
	public static class Code_PreInPosTraversal
	{
		//recursive adj.递归的，循环的 
		//先序递归 + 介绍
		public static void PreOrderRecur(Node head) {   // 1
			if (head == null) {
				return;
			}
			Console.WriteLine(head.value + " ");

			// 1 1段是第一次到此结点时
			PreOrderRecur(head.left);
			//2 
			//2 2段是左树遍历完后回到本体，此时是第二次到此结点(开始执行下一个语句)
			PreOrderRecur(head.right);
			//3
			//3 3段是第三此回到本体（发现下方没有语句了，自己才能返回）


			//1，2，3是递归序，之间可以干点什么也可以什么都不干
			//此函数在第一次打印此节点，形成先序，即对于所有子树来说都是先打印头结点再打印左子树所有节点然后打印右子树所有节点
			//那么中序（左 头 右）第二次来到此节点打印
			//后序（左 右 头）第三次来到此结点再打印
		}
		//中序递归
		public static void InOrderRecur(Node head) {
			if (head == null) {
				return;
			}
			InOrderRecur(head.left);
			Console.WriteLine(head.value + " ");
			InOrderRecur(head.right);
		}
		//后序递归
		public static void PosOrderRecur(Node head) {
			if (head == null) {
				return;
			}
			PosOrderRecur(head.left);
			PosOrderRecur(head.right);
			Console.WriteLine(head.value + " ");
		}

		//先序非递归
		public static void PreOrederUnRecur(Node head) {
			Console.WriteLine("pre-order: ");
			if (head != null) {
				Stack<Node> stack = new Stack<Node>();
				stack.Push(head);
				while (stack.Count != 0) {
					head = stack.Pop();
					Console.WriteLine(head.value + " ");
					if (head.right != null) {
						stack.Push(head.right);
					}
					if (head.left != null) {
						stack.Push(head.left);
					}
				}
			}
			Console.WriteLine();
		}
		//中序非递归
		public static void InOrderUnRecur(Node head) {
			Console.Write("in-order: ");
			if (head != null) {
				Stack<Node> stack = new Stack<Node>();
				while (stack.Count != 0 || head != null) {
					if (head != null) {//左边界全进栈
						stack.Push(head);
						head = head.left;
					} else {
						head = stack.Pop();
						Console.Write(head.value + " ");
						head = head.right;
					}
				}
			}
			Console.WriteLine();
		}
		//后序非递归
		public static void PosOrderUnRecur(Node head) {
			Console.Write("pos-order: ");
			if (head != null) {
				Stack<Node> stack1 = new Stack<Node>();
				Stack<Node> stack2 = new Stack<Node>();
				stack1.Push(head);
				while (stack1.Count != 0) {
					head = stack1.Pop();
					stack2.Push(head);
					if (head.left != null) {
						stack1.Push(head.left);
					}
					if (head.right != null) {
						stack1.Push(head.right);
					}
				}
				while (stack2.Count != 0) {
					Console.Write(stack2.Pop().value + " ");
				}
			}
			Console.WriteLine();
		}

	}

	//打印二叉树
	public static class Code_PrintBinaryTree
	{
		public static void PrintTree(Node head) {
			Console.WriteLine("Binary Tree:");
			PrintInOrder(head, 0, "H", 17);
			Console.WriteLine();
		}
		private static void PrintInOrder(Node head, int height, string to, int len) {
			if (head == null) {
				return;
			}
			PrintInOrder(head.right, height + 1, "v", len);
			string val = to + head.value + to;
			int LenM = val.Length;
			int LenL = (len - LenM) / 2;
			int LenR = len - LenM - LenL;
			val = GetSpace(LenL) + val + GetSpace(LenR);
			Console.WriteLine(GetSpace(height * len) + val);
			PrintInOrder(head.left, height + 1, "^", len);
		}
		private static string GetSpace(int num) {
			string space = " ";
			for (int i = 0; i < num; i++) {
				space += " ";
			}
			return space;
		}
	}

	//深度优先遍历就是先序，下方为宽度优先遍历
	public static class Code_TreeWidth
	{
		//打印宽度优先遍历
		public static void PrintWidthTree(Node head) {
			if (head == null) {
				return;
			}
			Queue<Node> queue = new Queue<Node>();
			queue.Enqueue(head);
			while (queue.Count != 0) {
				Node cur = queue.Dequeue();
				Console.Write(cur.value + " ");
				if (cur.left != null) {
					queue.Enqueue(cur.left);
				}
				if (cur.right != null) {
					queue.Enqueue(cur.right);
				}
			}
		}

		//返回所有层最大的节点数
		public static int WidthTreeMax1(Node head) {
			if (head == null) {
				return 0;
			}
			Queue<Node> queue = new Queue<Node>();
			queue.Enqueue(head);
			Hashtable LevelTable = new Hashtable();
			LevelTable.Add(head, 1);
			int CurLevel = 1;           //当前层数
			int CurLevelNodesNum = 0;   //当前层节点数
			int Max = int.MinValue;     //所有层最大的节点数
			while (queue.Count != 0) {
				Node cur = queue.Dequeue();
				//当前结点所在层数
				int CurNodeLevel = (int)LevelTable[cur];
				if (CurNodeLevel == CurLevel) {
					CurLevelNodesNum++;
				} else {
					Max = Math.Max(Max, CurLevelNodesNum);
					CurLevel++;
					CurLevelNodesNum = 1;
				}
				if (cur.left != null) {
					LevelTable.Add(cur.left, CurNodeLevel + 1);
					queue.Enqueue(cur.left);
				}
				if (cur.right != null) {
					LevelTable.Add(cur.right, CurNodeLevel + 1);
					queue.Enqueue(cur.right);
				}
			}
			return Max;
		}
		//同上，但不用哈希表
		public static int WidthTreeMax2(Node head) {
			if (head == null) {
				return 0;
			}
			Queue<Node> queue = new Queue<Node>();
			queue.Enqueue(head);
			Node CurEnd = head;//当前层最后结点
			Node NextCurEnd = null;//下一层最后结点
			int CurLevelNodesNum = 0;
			int Max = int.MinValue;
			while (queue.Count != 0) {
				Node Cur = queue.Dequeue();
				if (Cur.left != null) {
					queue.Enqueue(Cur.left);
					NextCurEnd = Cur.left;
				}
				if (Cur.right != null) {
					queue.Enqueue(Cur.right);
					NextCurEnd = Cur.right;
				}
				if (CurEnd != Cur) {
					CurLevelNodesNum++;
				} else {
					CurLevelNodesNum++;
					Max = Math.Max(Max, CurLevelNodesNum);
					CurEnd = NextCurEnd;
					NextCurEnd = null;
					CurLevelNodesNum = 0;
				}
			}
			return Max;
		}



	}

	//判断是否为平衡二叉树(二叉树递归套路，树型DP都可以用)
	public class BalancedTree
	{
		public static bool IsBalanced(Node head) {
			return Process(head).isBalanced;
		}
		private class ReturnType
		{
			public bool isBalanced;
			public int height;
			public ReturnType(bool isBalanced, int height) {
				this.isBalanced = isBalanced;
				this.height = height;
			}
		}
		private static ReturnType Process(Node x) {
			if (x == null) {
				return new ReturnType(true, 0);
			}
			ReturnType leftData = Process(x.left);
			ReturnType rightData = Process(x.right);
			int height = Math.Max(leftData.height, rightData.height) + 1;
			bool isBalanced = leftData.isBalanced && rightData.isBalanced
				&& Math.Abs(leftData.height - rightData.height) < 2;
			return new ReturnType(isBalanced, height);
		}
	}

	//判断是否搜索二叉树（Binary Search Tree）
	//二叉查找树又叫二叉搜索树、二叉排序树，
	//它或者是一棵空树，或者是具有下列性质的二叉树：
	//若它的左子树不空，则左子树上所有结点的值均小于它的根结点的值；
	//若它的右子树不空，则右子树上所有结点的值均大于它的根结点的值；
	//它的左、右子树也分别为二叉搜索树。
	public class IsBinarySearchTree
	{
		public static int predata = Int32.MinValue;
		//利用中序递归判断，如果是升序就是搜索二叉树
		public static bool IsBST(Node head) {
			if (head == null) {
				return true;
			}
			bool isLeftBST = IsBST(head.left);
			if (!isLeftBST) {
				return false;
			}
			//递归与非递归在中序遍历位置判断
			if (head.value <= predata) {
				return false;
			} else {
				predata = head.value;
			}
			//
			return IsBST(head.right);
		}


		//方法二：二叉树递归套路
		public class ReturnData
		{
			public bool isBST;
			public int min;
			public int max;
			public ReturnData(bool isBST, int min, int max) {
				this.isBST = isBST;
				this.min = min;
				this.max = max;
			}
		}
		public static ReturnData Process(Node x) {
			if (x == null) {
				return null;
			}
			ReturnData leftData = Process(x.left);
			ReturnData rightData = Process(x.right);
			int min = x.value;
			int max = x.value;
			if (leftData != null) {
				min = Math.Min(min, leftData.min);
				max = Math.Max(max, leftData.max);
			}
			if (rightData != null) {
				min = Math.Min(min, rightData.min);
				max = Math.Max(max, rightData.max);
			}
			//方法一 bool isBST = true;
			//if (leftData != null && (!leftData.isBST || leftData.max >= x.value)) {
			//	isBST = false;
			//}
			//if (rightData != null && (!rightData.isBST || x.value >= rightData.min)) {
			//	isBST = false;
			//}
			//方法二
			bool isBST = false;
			if (
				(leftData != null ? (leftData.isBST && leftData.max < x.value) : true)
				&&
				(rightData != null ?(rightData.isBST && rightData.min > x.value) : true)
				) {
				isBST = true;
			}
			return new ReturnData(isBST, min, max);
		}
	}

	//判断是否完全二叉树
	public class CompleteBinaryTree
	{
		public static bool IsCBT(Node head) {
			if (head == null) {
				return true;
			}
			Queue<Node> queue = new Queue<Node>();
			//是否遇到过左右两个孩子不双全的节点
			bool leaf = false;
			queue.Enqueue(head);
			while (queue.Count != 0) {
				Node cur = queue.Dequeue();

				//任一节点有右无左false
				//在上述不错条件下，如果遇到了第一个左右子树不全的节点，后续节点都是叶节点，若无false；
				if ((cur.left == null && cur.right != null)
					||
					(leaf &&(cur.left != null || cur.right != null))
					) {
					return false;
				}
				//

				if (cur.left != null) {
					queue.Enqueue(cur.left);
				}
				if (cur.right != null) {
					queue.Enqueue(cur.right);
				}

				//
				if (cur.left == null || cur.right == null) {
					leaf = true;
				}
				//
			}
			//
			return true;
			//
		}
	}

	//判断是否为满二叉树
	//-> 统计最大层数（深度）l再统计节点数n。如果满足公式n = 2^l - 1;成立
	public class FullBinaryTree
	{//二叉树递归套路
		public static bool IsFull(Node head) {
			if (head == null) {
				return true;
			}
			ReturnData allInfo = P(head);
			return (1 << allInfo.height - 1) == allInfo.nums;
		}
		private class ReturnData
		{
			public int height;
			public int nums;
			public ReturnData(int h, int n) {
				height = h;
				nums = n;
			}
		}
		private static ReturnData P(Node x) {
			if (x == null) {
				return new ReturnData(0, 0);
			}
			ReturnData leftData = P(x.left);
			ReturnData rightData = P(x.right);

			int height = Math.Max(leftData.height, rightData.height) + 1;

			int nums = leftData.nums + rightData.nums + 1;
			return new ReturnData(height, nums);
		}
	}
	
	//给定两个二叉树的节点o1和o2，找到他们的最低公共祖先节点
	public class LowestCommenAncestor
	{
		public static Node LowestAncestor(Node head, Node o1, Node o2) {
			if (head == null || head == o1 || head == o2) {
				return head;
			}
			Node left = LowestAncestor(head.left, o1, o2);
			Node right = LowestAncestor(head.right, o1, o2);

			if (left != null && right != null) {
				return head;
			}//左右两棵树，并不都有返回值进行下方
			return left != null ? left : right;
		}
	}

	//在Node2 二叉树中找到一个节点的后继节点
	//后继节点：中序遍历的序列中x的下一个节点是x的后继节点，中序遍历的最后一个节点的下一个节点（后继节点）是null
	//现在有Node2二叉树类型遍历中序不够优化
	//1）x有右树的时候，x的后继节点是右树的最左节点
	//2）x无右树，判断自己是不是父节点的左孩子，如果不是判断父节点是不是父父节点的左孩子，
	//如果不是继续向上判断，如果是则返回父父节点，因为相对父父节点来说x是父父节点左子树的最右节点
	//打印完x就该打印父父节点
	//另外，如果x是全树的最右节点则，x后继节点为null
	public class GetSuccessorNode
	{
		public static Node2 GetDescendantNode(Node2 node) {
			if (node == null) {
				return node;
			}
			if (node.right != null) {
				return GetLeftMost(node.right);
			} else {//无右子树
				Node2 parent = node.parent;
				while (parent != null && parent.left != node) {//当前节点是其父节点右孩子
					node = parent;
					parent = node.parent;
				}
				return parent;
			}
		}
		private static Node2 GetLeftMost(Node2 node) {
			if (node == null) {
				return node;
			}
			while (node.left != null) {
				node = node.left;
			}
			return node;
		}
	}

	//二叉树的序列化和反序列化
	//内存中的唯一的一棵树如何变唯一对应的字符串又如何反过来
	//如何判断一颗二叉树是不是另一颗二叉树的子树
	public class SerializeAndReconstructTree
	{
		//以head为头的树，序列化成字符串返回
		public static string SerializeByPre(Node head) {//以先序遍历
			if (head == null) {
				return "#_";
			}
			string res = head.value + "_";
			res += SerializeByPre(head.left);
			res += SerializeByPre(head.right);
			return res;
		}
		public static Node DeserializeByPreString(string preStr) {
			string[] values = preStr.Split("_");
			Queue<string> queue = new Queue<string>();
			for (int i = 0; i != values.Length; i++) {
				queue.Enqueue(values[i]);
			}
			return DeserializePreOrder(queue);
		}
		private static Node DeserializePreOrder(Queue<string> queue) {
			string value = queue.Dequeue();
			if (value.Equals("#")) {
				return null;
			}
			Node head = new Node(Convert.ToInt32(value));
			head.left = DeserializePreOrder(queue);
			head.right = DeserializePreOrder(queue);
			return head;
		}
	}

	//折纸问题
	public class PaperFolding
	{
		public static void PrintAllFolds(int N) {
			PrintProcess(1, N, true);
		}
		//递归过程，来到了某一个节点
		//i是节点的层数，N是一共的层数，down == true 凹 down == false 凸
		private static void PrintProcess(int i, int N, bool down) {
			if (i > N) {
				return;
			}
			PrintProcess(i + 1, N, true);
			Console.WriteLine(down ? "凹" : "凸");
			PrintProcess(i + 1, N, false);
		}
	}



	class Program
	{
		static void Main(string[] args) {
			Node head = new Node(1);
			head.left = new Node(2);
			head.right = new Node(3);
			head.left.left = new Node(4);
			head.right.left = new Node(5);
			head.right.right = new Node(6);
			head.left.left.right = new Node(7);
			head.left.left.right.left = new Node(8);
			//Code_PrintBinaryTree.PrintTree(head);
			//Code_TreeWidth.PrintWidthTree(head);
			//Code_PreInPosTraversal.PreOrderRecur(head);
			//Console.WriteLine(Code_TreeWidth.WidthTreeMax2(head));

			int N = 3;
			PaperFolding.PrintAllFolds(N);
		}
	}
}
