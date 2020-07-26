using BFS_c_sharp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFS_c_sharp.Methods
{
    public class ListOfFriendsAtDegree
    {
        public static List<UserNode> DisplayListOfFriendsAtDegree(List<UserNode> users, int sourceId, int degree)
        {
            // resulting list
            List<UserNode> result = new List<UserNode>();

            // instantiante the visited list and fill it with 0's - 0 = not visited, 1 = visited
            List<int> visited = Enumerable.Repeat(0, users.Count).ToList();

            // set up a queue and next wave of friends for the while loop
            List<int> queue = new List<int>();
            List<int> nextWave = new List<int>();
            visited[sourceId] = 1;
            nextWave.Add(sourceId);
            
            // calculation loop starting at 0 and ending at degree
            int startDegree = 0;
            do
            {
                startDegree++;
                Console.WriteLine($"Current queue size: {queue.Count}");
                queue.Clear();
                foreach (int userId in nextWave)
                {
                    foreach (UserNode friend in users.Find(u => u.Id == userId).Friends.ToList())
                    { 
                        if (visited[friend.Id] == 0)
                        {
                            visited[friend.Id] = 1;
                            queue.Add(friend.Id);      
                        }
                        // if this is the last wave, add all the friends of previous wave, without dupicates
                        if (startDegree == degree)
                        {
                            if (result.Find(f => f.Id == friend.Id) == null)
                            {
                                result.Add(friend);
                            }
                            
                        }
                    }
                }
                nextWave.Clear();
                foreach (int item in queue)
                {
                    nextWave.Add(item);
                }
                Console.WriteLine($"Exit queue size: {queue.Count}");
            } while (startDegree <= degree);

            Console.WriteLine($"Friends at degree {degree}");
            foreach (UserNode friendForDisplay in result)
            {
                Console.WriteLine($"{friendForDisplay.FirstName} {friendForDisplay.LastName}");
            }

            return result;
        }
    }
}
