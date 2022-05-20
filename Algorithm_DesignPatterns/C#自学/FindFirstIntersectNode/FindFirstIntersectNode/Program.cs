using System;

namespace FindFirstIntersectNode
{
    public class FindFirstIntersectNode
    {
        public class Node
        {
            public int value;
            public Node next;
            public Node(int value)
            {
                this.value = value;
            }
        }

        public static Node GetIntersectNode(Node head1, Node head2)
        {
            if (head1 == null || head2 == null)
            {
                return null;
            }
            Node loop1 = GetLoopNode(head1);
            Node loop2 = GetLoopNode(head2);
            if (loop1 == null && loop2 == null)
            {
                return NoLoopAndGetIntersectNode(head1, head2);
            }
            if (loop1 != null && loop2 != null)
            {
                return BothLoopAndGetIntersectNode(head1, loop1, head2, loop2);
            }
            //不可能有一个有环，一个无环
            return null;
        }

        //找到链表第一个入环节点， 如果无环，返回null
        public static Node GetLoopNode(Node head)
        {
            if (head == null || head.next == null || head.next.next == null)
            {
                return null;
            }
            Node n1 = head.next;//慢指针走一步
            Node n2 = head.next.next; //快指针走两步
            while (n1 != n2)//如果有环，一定会在环内相遇
            {
                if (n2.next == null || n2.next.next == null)
                {
                    return null;
                }
                n2 = n2.next.next;
                n1 = n1.next;
            }
            n2 = head;//让快指针重新指向头结点
            while (n1 != n2)
            {//快慢指针都一次走一步
                n1 = n1.next;
                n2 = n2.next;
            }//相遇时所在结点就是第一个入环结点
            return n1;
        }

        //如果两个链表都无环，返回第一个相交结点，如果不相交，返回null
        public static Node NoLoopAndGetIntersectNode(Node head1, Node head2)
        {
            if (head1 == null || head2 == null)
            {
                return null;
            }
            Node cur1 = head1;
            Node cur2 = head2;
            int n = 0;
            while (cur1.next != null)
            {
                n++;//计算此链表length
                cur1 = cur1.next;
            }
            while (cur2.next != null)
            {
                n--;//计算与上一个链表length之差
                cur2 = cur2.next;
            }
            if (cur1 != cur2)
            {
                return null;
            }
            cur1 = n > 0 ? head1 : head2;//谁长，谁的头变成cur1
            cur2 = cur1 == head1 ? head2 : head1;//谁短，谁的头变成cur2
            n = Math.Abs(n);
            //使较长的cur1走到 与相交结点的距离，和cur2头结点与相交结点的距离 一样的结点
            while (n != 0)
            {
                n--;
                cur1 = cur1.next;
            }
            while (cur1 != cur2)
            {//一起走都一次走一步，相遇所在的结点就是相交结点
                cur1 = cur1.next;
                cur2 = cur2.next;
            }
            return cur1;
        }

        //两个有环链表， 返回第一个相关结点， 如果不相交返回null
        public static Node BothLoopAndGetIntersectNode(Node head1, Node loop1, Node head2, Node loop2)
        {
            Node cur1 = null;
            Node cur2 = null;
            if (loop1 == loop2)
            {//与NoLoopAndGetIntersectNode思路一样只是结尾是入环节点不是null
                cur1 = head1;
                cur2 = head2;
                int n = 0;
                while (cur1 != loop1)
                {
                    n++;
                    cur1 = cur1.next;
                }
                while (cur2 != loop2)
                {
                    n--;
                    cur2 = cur2.next;
                }
                cur1 = n > 0 ? head1 : head2;
                cur2 = cur1 == head1 ? head2 : head1;
                n = Math.Abs(n);
                while (n != 0)
                {
                    n--;
                    cur1 = cur1.next;
                }
                while (cur1 != cur2)
                {
                    cur1 = cur1.next;
                    cur2 = cur2.next;
                }
                return cur1;
            }
            else
            {
                cur1 = loop1.next;//cur1从loop1开始走
                while (cur1 != loop1)
                {
                    if (cur1 == loop2)//直到遇到loop2
                    {
                        return loop1;
                    }
                    cur1 = cur1.next;
                }//转一圈没看到loop2
                return null;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
