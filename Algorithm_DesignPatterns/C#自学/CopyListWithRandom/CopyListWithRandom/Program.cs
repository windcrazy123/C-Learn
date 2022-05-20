using System;
using System.Collections;
using System.Collections.Generic;

namespace CopyListWithRandom
{
    public class CopyListWithRandom
    {
        public class Node
        {
            public int value;
            public Node next;
            public Node rand;
            public Node(int value)
            {
                this.value = value;
            }
        }

        //哈希表
        public static Node CopyRandomList1(Node head)
        {
            Dictionary<Node, Node> table = new Dictionary<Node, Node>();
            Node cur = head;
            while (cur != null)
            {
                table.Add(cur, new Node(cur.value));
                cur = cur.next;
            }
            cur = head;
            while (cur != null)
            {//   cur     old
             //table[cur] new
                table[cur].next = table[cur.next];
                table[cur].rand = table[cur.rand];
                cur = cur.next;
            }
            return table[head];
        }
        //完全等同方法一
        /*
        public static Node CopyRandomList3(Node head)
        {
            Hashtable table = new Hashtable();
            Node cur = head;
            while (cur != null)
            {
                table.Add(cur, new Node(cur.value));
                cur = cur.next;
            }
            cur = head;
            while (cur != null)
            {//   cur     old
             //table[cur] new
                ((Node)table[cur]).next = (Node)table[cur.next];
                ((Node)table[cur]).rand = (Node)table[cur.rand];
                cur = cur.next;
            }
            
            return (Node)table[head];
        }*/

        //回溯+哈希表
        public static Node CopyRandomList2(Node head)
        {
            Dictionary<Node, Node> table = new Dictionary<Node, Node>();
            if (head == null)
            {
                return null;
            }
            if (!table.ContainsKey(head))
            {
                Node headNew = new Node(head.value);
                table.Add(head, headNew);
                headNew.next = CopyRandomList1(head.next);
                headNew.rand = CopyRandomList1(head.rand);
            }
            return table[head];
        }

        //迭代 + 节点拆分
        public Node CopyRandomList3(Node head)
        {
            if (head == null)
            {
                return null;
            }
            for (Node node = head; node != null; node = node.next.next)
            {
                Node nodeNew = new Node(node.value)
                {
                    next = node.next
                };//相当于
                  //Node nodeNew = new Node(node.value);
                //nodeNew.next = node.next;
                node.next = nodeNew;
            }
            for (Node node = head; node != null; node = node.next.next)
            {
                Node nodeNew = node.next;
                nodeNew.rand = (node.rand != null) ? node.rand.next : null;
            }
            Node headNew = head.next;
            for (Node node = head; node != null; node = node.next)
            {
                Node nodeNew = node.next;
                node.next = node.next.next;
                nodeNew.next = (nodeNew.next != null) ? nodeNew.next.next : null;
            }
            return headNew;
        }
        //同种思路
        public static Node CopyRandomList4(Node head)
        {
            if (head == null)
            {
                return null;
            }
            Node cur = head;
            Node next = null;
            //copy node and link to every node
            //1->2->3               原链表
            //1->1'->2->2'->3->3'   新链表
            while (cur != null)
            {
                next = cur.next;
                cur.next = new Node(cur.value);
                cur.next.next = next;
                cur = next;
            }
            cur = head;
            Node CurCopy = null;
            //set copy node's rand
            while (cur != null)
            {
                next = cur.next.next;
                CurCopy = cur.next;
                CurCopy.rand = cur.rand != null ? cur.rand.next : null;
                cur = next;
            }
            Node res = head.next;
            cur = head;
            //split
            while (cur != null)
            {
                next = cur.next.next;
                CurCopy = cur.next;
                cur.next = next;
                CurCopy.next = (next != null) ? next.next : null;
                cur = next;
            }
            return res;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            
        }
    }
}
