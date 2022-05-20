using System;

namespace Partition_List
{
    public class Partition
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

        //不考虑空间，但是思路简单，容易写（外加数组，将数组partition）
        #region
        public static Node ListPartition1(Node head, int pivot)
        {
            if (head == null)
            {
                return head;
            }
            Node cur = head;
            int i = 0;
            while (cur != null)
            {
                i++;
                cur = cur.next;
            }
            Node[] nodeArr = new Node[i];
            //i = 0;
            cur = head;
            for (i = 0; i != nodeArr.Length; i++)
            {
                nodeArr[i] = cur;
                cur = cur.next;
            }
            ArrPartition(nodeArr, pivot);
            for (i = 1; i != nodeArr.Length; i++)
            {
                nodeArr[i - 1].next = nodeArr[i];
            }
            nodeArr[i - 1].next = null;
            return nodeArr[0];
        }
        private static void ArrPartition(Node[] nodeArr, int pivot)
        {
            int small = -1;
            int big = nodeArr.Length;
            int index = 0;
            while (index != big)
            {
                if (nodeArr[index].value < pivot)
                {
                    swap(nodeArr, ++small, index++);
                }
                else if (nodeArr[index].value == pivot)
                {
                    index++;
                }
                else
                {
                    swap(nodeArr, --big, index);
                }
            }
        }
        private static void swap(Node[] nodeArr, int a, int b)
        {
            Node tmp = nodeArr[a];
            nodeArr[a] = nodeArr[b];
            nodeArr[b] = tmp;
        }
        #endregion

        //need O(1) extra space
        public static Node ListPartition(Node head, int pivot)
        {
            Node SH = null;//small head
            Node ST = null;//small tail
            Node EH = null;//equal head
            Node ET = null;//equal tail
            Node MH = null;//big head
            Node MT = null;//big tail
            Node next = null;//save next node
            //every node distributed to three lists
            while (head != null)
            {
                next = head.next;
                head.next = null;
                if (head.value < pivot)
                {
                    if (SH == null)
                    {
                        SH = head;
                        ST = head;
                    }
                    else
                    {
                        ST.next = head;
                        ST = head;
                    }
                }
                else if (head.value == pivot)
                {
                    if (EH == null)
                    {
                        EH = head;
                        ET = head;
                    }
                    else
                    {
                        ET.next = head;
                        ET = head;
                    }
                }
                else
                {
                    if (MH == null)
                    {
                        MH = head;
                        MT = head;
                    }
                    else
                    {
                        MT.next = head;
                        MT = head;
                    }
                }
                head = next;
            }
            //small and equal reconnect
            if (ST != null)//如果有小于区域
            {
                ST.next = EH;
                ET = ET == null ? ST : ET;//下一步，谁去连大于区域的头，谁就变成ET
            }
            //上面的if，不管跑了没有
            //如果小于区域和等于区域，不是都没有
            if (ET != null)
            {
                ET.next = MH;
            }
            return SH != null ? SH : (EH != null ? EH : MH);
        }



        
        static void Main(string[] args)
        {
            
        }
    }
    class Program
    {
        
    }
}
