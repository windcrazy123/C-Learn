using System;
using System.Collections.Generic;

namespace IsPalindrome_List
{
    public class IsPalindrome
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

        //need n extra space全压栈里
        public bool isPalindrome1(Node head)
        {
            //准备一个栈
            Stack<Node> stack = new Stack<Node>();
            Node cur = head;
            while (cur != null)
            {//把链表数都压到栈里
                stack.Push(cur);
                cur = cur.next;
            }
            while (head != null)
            {//栈弹一个value，就跟链表一个value比对（从前往后）
                if (head.value != stack.Pop().value)
                {
                    return false;
                }
                head = head.next;
            }
            return true;
        }
        
        //need n/2 extra space后一半压栈里（不包括中间的）
        /*
        public bool isPalindrome2(Node head)
        {
            if (head == null || head.next == null)
            {
                return true;
            }
            Node right = head.next;
            Node cur = head;
            while (cur.next != null && cur.next.next != null)
            {
                right = right.next;
                cur = cur.next.next;
            }
            Stack<Node> stack = new Stack<Node>();
            while (right != null)
            {
                stack.Push(right);
                right = right.next;
            }
            while (stack.Count != 0)
            {
                if (head.value != stack.Pop().value)
                {
                    return false;
                }
                head = head.next;
            }
            return true;
        }
        */

        //need O(1) extra space
        public static bool isPalindrome3(Node head)
        {
            if (head == null || head.next == null)
            {
                return true;
            }
            Node n1 = head;
            Node n2 = head;
            //find mid node
            while (n2.next != null && n2.next.next != null)
            {
                n1 = n1.next;//n1 -> mid
                n2 = n2.next.next;//n2 -> end
            }
            n2 = n1.next;//n2 -> right part first node
            n1.next = null;//mid.next -> null
            Node n3 = null;
            //right part convert
            while (n2 != null)
            {
                n3 = n2.next;//n3 -> save next node
                n2.next = n1;//next of right node convert
                n1 = n2;//n1 move
                n2 = n3;//n2 move
            }
            n3 = n1;//n3 -> save last node
            n2 = head;//n2 -> left first node
            bool res = true;
            //check palindrome
            while (n1 != null && n2 != null)
            {
                if (n1.value != n2.value)
                {
                    res = false;
                    break;
                }
                n1 = n1.next;//left to mid
                n2 = n2.next;//right to mid
            }
            n1 = n3.next;
            n3.next = null;
            //recover list
            while (n1 != null)
            {
                n2 = n1.next;
                n1.next = n3;
                n3 = n1;
                n1 = n2;
            }
            return res;
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
