using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SinglyLinkedLists
{
    public class SinglyLinkedList
    {

        private SinglyLinkedListNode firstNode;

        public SinglyLinkedList()
        {
            // NOTE: This constructor isn't necessary, once you've implemented the constructor below.
        }

        // READ: http://msdn.microsoft.com/en-us/library/aa691335(v=vs.71).aspx
        public SinglyLinkedList(params object[] values)
        {
            foreach (object i in values)
            {
                string iString = i.ToString();
                this.AddLast(iString);
            }
        }

        // READ: http://msdn.microsoft.com/en-us/library/6x16t2tx.aspx
        public string this[int i]
        {
            get { return ElementAt(i); }
            set 
            {
                SinglyLinkedListNode nodei = this.firstNode;
                for (int j = 0; j < i - 1; j++)
                {
                    nodei = nodei.Next;
                }
                SinglyLinkedListNode oldNext = nodei.Next.Next;
                SinglyLinkedListNode insertedNode = new SinglyLinkedListNode(value);
                nodei.Next = insertedNode;
                insertedNode.Next = oldNext;
            }
        }

        

        public void AddAfter(string existingValue, string value)
        {
            SinglyLinkedListNode nodei = this.firstNode;
            SinglyLinkedListNode prevNext = nodei.Next;
            SinglyLinkedListNode newNode = new SinglyLinkedListNode(value);
            while (nodei.Value != existingValue)
            {
                nodei = nodei.Next;
                if (nodei == null)
                {
                    throw new ArgumentException();
                }
            }
            if (nodei.IsLast())
            {
                this.AddLast(value);
                
            }
            else
            {
                nodei.Next = newNode;
                newNode.Next = prevNext;
                
            }

        }

        public void AddFirst(string value)
        {
            SinglyLinkedListNode prevFirst = this.firstNode;
            SinglyLinkedListNode newFirst = new SinglyLinkedListNode(value);
            this.firstNode = newFirst;
            newFirst.Next = prevFirst;
            
        }

        public void AddLast(string value)
        {
            if (firstNode == null)
            {
                firstNode = new SinglyLinkedListNode(value);
            }
            else
            {
                SinglyLinkedListNode nodei = this.firstNode;

                while (!nodei.IsLast())
                {
                    nodei = nodei.Next;
                }
                nodei.Next = new SinglyLinkedListNode(value);
            }
        }

        // NOTE: There is more than one way to accomplish this.  One is O(n).  The other is O(1).
        public int Count()
        {
            SinglyLinkedListNode nodei = this.firstNode;
            int counter = 0;
            while (nodei != null)
            {
                counter++;
                nodei = nodei.Next;
            }
            return counter;
        }

        public string ElementAt(int index)
        {
            SinglyLinkedListNode nodei = this.firstNode;

            for (int i = 0; i < index; i++)
            {
                nodei = nodei.Next;
                if (nodei == null)
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
            if (nodei == null)
            {
                throw new ArgumentOutOfRangeException();
            }
            return nodei.Value;
        }

        public string First()
        {
            if (firstNode == null)
            {
                return null;
            }
            else
            {
                return firstNode.Value;
            }
        }

        public int IndexOf(string value)
        {
            if (this.firstNode == null)
            {
                return -1;
            }
            int counter = 0;
            SinglyLinkedListNode nodei = this.firstNode;
            while (nodei.Value != value)
            {
                nodei = nodei.Next;
                counter++;
                if (nodei == null)
                {
                    return -1;
                }
            }
            return counter;
        }

        public bool IsSorted()
        {
            SinglyLinkedListNode current = firstNode;
            if (current == null || current.Next == null)
            {
                return true;
            }
            for (int i = 0; i < Count() - 1; i++)
            {
                if (current < current.Next || current.Value == current.Next.Value)
                {
                    current = current.Next;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        // HINT 1: You can extract this functionality (finding the last item in the list) from a method you've already written!
        // HINT 2: I suggest writing a private helper method LastNode()
        // HINT 3: If you highlight code and right click, you can use the refactor menu to extract a method for you...
        public string Last()
        {
            SinglyLinkedListNode nodei = this.firstNode;
            if (nodei == null)
            {
                return null;
            }

            while (!nodei.IsLast())
            {
                nodei = nodei.Next;
            }
            return nodei.Value;
        }

        public void Remove(string value)
        {
            SinglyLinkedListNode nodei = this.firstNode;
            if (nodei.Value == value)
            {
                firstNode = nodei.Next;
            }

            int index = this.IndexOf(value);
            
            for (int i = 0; i < index - 1; i++)
            {
                nodei = nodei.Next;
            }
            if (index >= 0)
            {
                nodei.Next = nodei.Next.Next;
            } 
        }

        private SinglyLinkedListNode NodeAt(int index) 
        {
            SinglyLinkedListNode current = firstNode;
            for (int i = 0; i < index; i++)
			{
                current = current.Next;
			}
            return current;
        }

        public void Swap(int index1, int index2)
        {
            SinglyLinkedListNode node1 = new SinglyLinkedListNode(NodeAt(index1).Value);
            SinglyLinkedListNode node2 = new SinglyLinkedListNode(NodeAt(index2).Value);
            SinglyLinkedListNode node1Next = NodeAt(index1 + 1);
            SinglyLinkedListNode node1Prev = NodeAt(index1 - 1);
            SinglyLinkedListNode node2Next = NodeAt(index2 + 1);
            SinglyLinkedListNode node2Prev = NodeAt(index2 - 1);

            node1Prev.Next = node2;
            node2.Next = node1Next;
            node2Prev.Next = node1;
            node1.Next = node2Next;
        }

        public void Sort()
        {
            int listLength = Count();
            for (int i = 0; i < listLength; i++)
            {
                SinglyLinkedListNode curr = NodeAt(i);
                SinglyLinkedListNode lowest = curr;
                for (int j = i+1; j < listLength; j++)
                {
                    if (NodeAt(j) < lowest)
                    {
                        lowest = NodeAt(j);
                    }
                }
            }
        }

        public string[] ToArray()
        {
            string[] splitters = new string[] { ",", " ", "{", "}", "\"" };
            string longStr = this.ToString();
            string[] list = longStr.Split(splitters, StringSplitOptions.RemoveEmptyEntries);

            return list;
        }
        public override string ToString()
        {
            
            SinglyLinkedListNode nodei = this.firstNode;
            var strBuilder = new System.Text.StringBuilder();
            strBuilder.Append("{ ");
            if (nodei == null)
            {
                strBuilder.Append("}");
                return strBuilder.ToString();
            }
            while (!nodei.IsLast())
            {
                strBuilder.Append("\"" + nodei.Value + "\", ");
                nodei = nodei.Next;
            }
            strBuilder.Append("\"" + nodei.Value + "\" }");
            return strBuilder.ToString();
        }
    }
}
