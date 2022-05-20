using System;

namespace LC744
{
    class Program
    {
        static void Main(string[] args)
        {
            char output;
            char[] array = { 'a', 'b', 'c', 'e', 'f' };
            Seach seach = new Seach();
            output = seach.BinarySeach(array, 'd');
            Console.WriteLine(output);
        }
    }
    class Seach
    {
        public char BinarySeach(char[] letters, char target)
        {
            int min = 0;
            int max = letters.Length - 1;
            int left = 0;
            int right = letters.Length - 1;
            if ((letters[min] > target) || (letters[max] <= target))
            {
                return letters[min];
            }
            else {
                while (left <= right)
                {
                    int mid = left + (right - left) / 2;
                    if ((letters[mid] <= target) && (letters[mid + 1] > target))
                    {
                        return letters[mid + 1];
                    } else if (target < letters[mid])
                    {
                        right = mid - 1;
                    }
                    else
                    {
                        left = mid + 1;
                    }
                }
            }
            return '!';
        }
    }
    /*
     public class Solution {
        public char NextGreatestLetter(char[] A, char target) {
            int low=0,high=A.Length-1;
            char R='-'; 
            while(low<=high)
            {
                int mid=low+(high-low)/2;
                if(A[mid] > target)
                {
                    high=mid-1;
                    R=A[mid];
                }
                else
                {
                    low=mid+1;
                }   
            }
            return R=='-'?A[0]:R;
        }
    }
     */
    /*public class Solution
    {
        public char NextGreatestLetter(char[] letters, char target)
        {
            int lo = 0;
            int hi = letters.Length - 1;
            int targetIndex = -1;
            while (lo <= hi)
            {
                int mi = lo + (hi - lo) / 2;
                // If current is smaller or equal than target
                // Keep this index and continue search in right part
                if (letters[mi] <= target)
                {
                    targetIndex = mi;
                    lo = mi + 1;
                }
                else
                {
                    hi = mi - 1;
                }
            }

            int next = (targetIndex + 1) % letters.Length;
            return letters[next];
        }
    }*/
}
