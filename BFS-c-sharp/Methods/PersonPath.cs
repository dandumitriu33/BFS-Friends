using BFS_c_sharp.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFS_c_sharp.Methods
{
    public class PersonPath
    {
        public static void PrintShortestParthAndPeople(List<UserNode> users, int sourceId, int destinationId)
        {
            bool pathFound = false;
            // resulting list
            List<UserNode> result = new List<UserNode>();

            // instantiante the sourceVisited list and fill it with 0's - 0 = not visited, 1 = visited
            List<int> sourceVisited = Enumerable.Repeat(0, users.Count).ToList();

            // set up a Source queue and next wave of friends for the while loop
            List<UserNode> sourceQueue = new List<UserNode>();
            List<UserNode> sourceNextWave = new List<UserNode>();
            UserNode sourceUser = users.Find(u => u.Id == sourceId);
            sourceVisited[sourceUser.Id] = 1;
            UserNode sourcePredecessor = new UserNode();
            sourcePredecessor.Id = -1;
            sourceUser.SourcePredecessor = sourcePredecessor;
            sourceNextWave.Add(sourceUser);
            

            // instantiante the destinationVisited list and fill it with 0's - 0 = not visited, 1 = visited
            List<int> destinationVisited = Enumerable.Repeat(0, users.Count).ToList();

            // set up a Destination queue and next wave of friends for the while loop
            List<UserNode> destinationQueue = new List<UserNode>();
            List<UserNode> destinationNextWave = new List<UserNode>();
            UserNode destinationUser = users.Find(u => u.Id == destinationId);
            destinationVisited[destinationUser.Id] = 1;
            UserNode destinationPredecessor = new UserNode();
            destinationPredecessor.Id = -1;
            destinationUser.DestinationPredecessor = destinationPredecessor;
            destinationNextWave.Add(destinationUser);

            displayUserFriends(sourceUser, destinationUser);
            bool pathWasPrinted = false;

            // go through every user in sourceNextWave and add Predecessor and compare against destinationVisited
            // if exists in destinationVisited - path is found
            // same with destination and sourceVisited

            while (pathFound == false)
            {
                //if (sourceVisited.Count == users.Count || destinationVisited.Count == users.Count)
                //{
                //    pathFound = true;
                //    Console.WriteLine($"Nodes exhausted without finding a path betweehn {sourceId} and {destinationId}");
                //    break;
                //}

                // first goes source
                sourceQueue.Clear();
                foreach (var friend in sourceNextWave)
                {
                    if (destinationVisited[friend.Id] == 1)
                    {
                        pathFound = true;
                        if (pathWasPrinted == false)
                        {
                            BuildPathAndPrint(users, friend);
                            pathWasPrinted = true;
                        }
                        break;
                    }
                    else
                    {
                        foreach (UserNode friendOfFriend in friend.Friends)
                        {
                            if (sourceVisited[friendOfFriend.Id] == 0)
                            {
                                friendOfFriend.SourcePredecessor = friend;
                                sourceQueue.Add(friendOfFriend);
                                sourceVisited[friendOfFriend.Id] = 1;
                            }   
                        }
                    }
                }
                sourceNextWave.Clear();
                foreach (UserNode user in sourceQueue)
                {
                    sourceNextWave.Add(user);
                }

                // 2nd goes destination
                destinationQueue.Clear();
                foreach (UserNode friend in destinationNextWave)
                {
                    if (sourceVisited[friend.Id] == 1)
                    {
                        pathFound = true;
                        if (pathWasPrinted == false)
                        {
                            BuildPathAndPrint(users, friend);
                            pathWasPrinted = true;
                        }
                        break;
                    }
                    else
                    {
                        foreach (UserNode friendOfFriend in friend.Friends)
                        {
                            if (destinationVisited[friendOfFriend.Id] == 0)
                            {
                                friendOfFriend.DestinationPredecessor = friend;
                                destinationQueue.Add(friendOfFriend);
                                destinationVisited[friendOfFriend.Id] = 1;
                            }
                        }
                    }
                }
                destinationNextWave.Clear();
                foreach (UserNode user in destinationQueue)
                {
                    destinationNextWave.Add(user);
                }
            }

        }

        private static void displayUserFriends(UserNode sourceUser, UserNode destinationUser)
        {
            Console.WriteLine($"Source User's friends:");
            foreach (var item in sourceUser.Friends)
            {
                Console.WriteLine($"{item.FirstName} {item.LastName}");
            }
            Console.WriteLine($"Destination User's friends:");
            foreach (var item in destinationUser.Friends)
            {
                Console.WriteLine($"{item.FirstName} {item.LastName}");
            }
        }

        private static void BuildPathAndPrint(List<UserNode> users, UserNode friend)
        {
            int currentSourcePredecessorId = friend.SourcePredecessor.Id;
            int currentDestinationPredecessorId = friend.DestinationPredecessor.Id;
            List<UserNode> shortestPath = new List<UserNode>();
            shortestPath.Add(friend);
            while (currentSourcePredecessorId != -1 && currentDestinationPredecessorId != -1)
            {
                if (currentSourcePredecessorId != -1)
                {
                    UserNode userToInsert = users.Find(u => u.Id == currentSourcePredecessorId);
                    shortestPath.Insert(0, userToInsert);
                    currentSourcePredecessorId = userToInsert.SourcePredecessor.Id;
                }
                if (currentDestinationPredecessorId != -1)
                {
                    UserNode userToAdd = users.Find(u => u.Id == currentDestinationPredecessorId);
                    shortestPath.Add(userToAdd);
                    currentDestinationPredecessorId = userToAdd.DestinationPredecessor.Id;
                }
            }
            foreach (UserNode pathFriend in shortestPath)
            {
                Console.WriteLine($"Node {pathFriend.Id} - {pathFriend.FirstName} {pathFriend.LastName}");
            }
        }
    }
}
