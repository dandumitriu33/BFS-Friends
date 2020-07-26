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
            int distance = 0;
            bool destinationFound = false;
            List<int> visited = new List<int>();
            for (int i = 0; i < users.Count; i++)
            {
                visited.Add(0);
            }
            List<int> queue = new List<int>();
            List<int> nextWave = new List<int>();
            visited[sourceId] = 1;
            nextWave.Add(sourceId);
            Console.WriteLine("Pre while...");
            while (destinationFound == false)
            {
                if (AllVisited(visited))
                {
                    destinationFound = true;
                    Console.WriteLine("All users visited. destinationFound set to true.");
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
