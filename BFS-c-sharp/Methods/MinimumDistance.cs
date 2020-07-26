using BFS_c_sharp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFS_c_sharp.Methods
{
    public class MinimumDistance
    {
        public static int DisplayShortestDistance(List<UserNode> users, int sourceId, int destinationId)
        {
            displayUserDetails(users, sourceId, destinationId);
            int distance = 0;
            bool destinationFound = false;

            // instantiante the visited list and fill it with 0's - 0 = not visited, 1 = visited
            List<int> visited = Enumerable.Repeat(0, users.Count).ToList();

            // set up a queue and next wave of friends for the while loop
            List<int> queue = new List<int>();
            List<int> nextWave = new List<int>();
            visited[sourceId] = 1;
            nextWave.Add(sourceId);
            
            // go through each wave / level of friends from source to destination every while loop
            while (destinationFound == false)
            {
                if (AllVisited(visited))
                {
                    destinationFound = true;
                    Console.WriteLine("All users visited. destinationFound set to true.");
                    break;
                }
                queue.Clear();
                distance++;

                foreach (int item in nextWave)
                {
                    queue.Add(item);
                }
                nextWave.Clear();

                Console.WriteLine(queue.Count);
                Console.ReadKey();

                foreach (int currentUserId in queue)
                {
                    Console.WriteLine($"Looking at user {currentUserId} in queue");
                    UserNode currentUser = users.FirstOrDefault(u => u.Id == currentUserId);
                    Console.WriteLine($"User recevied {currentUser.FirstName}");
                    if (currentUser != null)
                    {
                        foreach (var friend in currentUser.Friends)
                        {
                            if (friend.Id == destinationId)
                            {
                                destinationFound = true;
                            }
                            else if (visited[friend.Id] == 0)
                            {
                                nextWave.Add(friend.Id);
                                visited[friend.Id] = 1;
                            }
                        }
                    }  
                }
                Console.WriteLine($"NextWave len {nextWave.Count}");
            }
            return distance;
        }

        private static void displayUserDetails(List<UserNode> users, int sourceId, int destinationId)
        {
            Console.WriteLine($"Source user's ID: {sourceId} {users[sourceId].FirstName} {users[sourceId].LastName} and has {(users.Find(u => u.Id == sourceId)).Friends.Count} friends");
            foreach (var item in users[sourceId].Friends)
            {
                Console.WriteLine($"Friend ID: {item.Id} {item.FirstName} {item.LastName}");
            }
            
            Console.WriteLine($"Destination user's ID: {destinationId} {users[destinationId].FirstName} {users[destinationId].LastName} and has {(users.Find(u => u.Id == destinationId)).Friends.Count} friends");
            foreach (var item in users[destinationId].Friends)
            {
                Console.WriteLine($"Friend ID: {item.Id} {item.FirstName} {item.LastName}");
            }
        }

        private static bool AllVisited(List<int> visited)
        {
            if (visited.Find(el => el == 0) == 0)
            {
                return false;
            }
            return true;
        }
    }
}
