using System;
using System.Xml.Linq;

namespace SmartSeats.lk
{
	public class Route
	{
        public Junction? Departure;
        public Junction? Arrival;
        public int NoOfJunctions;

        public Route()
        {
            Departure = null;
            Arrival = null;
            NoOfJunctions = 0;
        }

        public Route(Route route)
        {
            Junction current = route.Departure;
            while(current != null)
            {
                Junction temp = new Junction(current.key);

                //For empty route
                if (Arrival == null)
                {
                    Arrival = temp;
                    Departure = temp;
                    NoOfJunctions++;
                }

                //for non-empty route
                else
                {
                    Arrival.next = temp;
                    Arrival = temp;
                    NoOfJunctions++;
                }
                current = current.next;
            }
            
        }

        //Add junctions to the last of the route
        public void SetRoute(string[] val)
        {
            for(int i = 0; i < val.Length; i++)
            {
                //Create a Junction node
                Junction temp = new Junction(val[i]);

                //For empty route
                if (Arrival == null)
                {
                    Arrival = temp;
                    Departure = temp;
                    NoOfJunctions++;
                }

                //for non-empty route
                else
                {
                    Arrival.next = temp;
                    Arrival = temp;
                    NoOfJunctions++;
                }
            }
        }

        public void AddToRoute(string val)
        {
            //Create a Junction node
            Junction temp = new Junction(val);

            //For empty route
            if (Arrival == null)
            {
                Arrival = temp;
                Departure = temp;
                NoOfJunctions++;
            }

            //for non-empty route
            else
            {
                Arrival.next = temp;
                Arrival = temp;
                NoOfJunctions++;
            }
        }

        public void PrintRoute()
        {
            if (Departure == null)
            {
                Console.WriteLine("No avilable route");
                return;
            }

            else
            {
                Junction current = Departure;

                while (current != null)
                {
                    Console.Write(current.key);
                    if (current.next != null) // Check if there's a next node
                    {
                        Console.Write(" -> ");
                    }
                    current = current.next;
                }
            }
        }

        public void DeleteAllRoutes()
        {
            Departure = null;
            Arrival = null;
            NoOfJunctions = 0;
        }
        /*
        public void BubbleSort()
        {
            if (Departure == null || Departure.next == null)
            {
                return; // No sorting needed for an empty list or a single-node list
            }

            bool swapped;
            do
            {
                swapped = false;
                Junction current = Departure;
                Junction previous = null;

                while (current.next != null)
                {
                    // Compare adjacent nodes using string.Compare
                    if (string.Compare(current.key, current.next.key, StringComparison.Ordinal) > 0)
                    {
                        // Swap the nodes
                        Junction nextNode = current.next;
                        current.next = nextNode.next;
                        nextNode.next = current;

                        if (previous == null)
                        {
                            Departure = nextNode; // Update the head if the first node is swapped
                        }
                        else
                        {
                            previous.next = nextNode; // Update the previous node's next reference
                        }

                        previous = nextNode;
                        swapped = true;
                    }
                    else
                    {
                        previous = current;
                        current = current.next;
                    }
                }
            } while (swapped); // Repeat until no more swaps are needed
        }
        */

        public void MergeSort()
        {
            Departure = MergeSortHelper(Departure);
        }

        // Helper method to perform Merge Sort recursively
        private Junction MergeSortHelper(Junction head)
        {
            // Base case: if the list is empty or has only one node, it is already sorted
            if (head == null || head.next == null)
            {
                return head;
            }

            // Split the list into two halves
            Junction middle = GetMiddle(head);
            Junction nextOfMiddle = middle.next;
            middle.next = null; // Break the list into two halves

            // Recursively sort the two halves
            Junction left = MergeSortHelper(head);
            Junction right = MergeSortHelper(nextOfMiddle);

            // Merge the two sorted halves
            return Merge(left, right);
        }

        // Helper method to get the middle node of the linked list
        private Junction GetMiddle(Junction head)
        {
            if (head == null)
            {
                return head;
            }

            Junction slow = head;
            Junction fast = head.next;

            // Move 'fast' two steps and 'slow' one step at a time
            // When 'fast' reaches the end, 'slow' will be at the middle
            while (fast != null && fast.next != null)
            {
                slow = slow.next;
                fast = fast.next.next;
            }

            return slow;
        }

        // Helper method to merge two sorted linked lists
        private Junction Merge(Junction left, Junction right)
        {
            Junction result = null;

            // Base cases
            if (left == null)
            {
                return right;
            }
            if (right == null)
            {
                return left;
            }

            // Compare the nodes and merge them in sorted order
            if (string.Compare(left.key, right.key, StringComparison.Ordinal) <= 0)
            {
                result = left;
                result.next = Merge(left.next, right);
            }
            else
            {
                result = right;
                result.next = Merge(left, right.next);
            }

            return result;
        }
    }

}

