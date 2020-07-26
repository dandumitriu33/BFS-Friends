using BFS_c_sharp.Methods;
using BFS_c_sharp.Model;
using System;
using System.Collections.Generic;

namespace BFS_c_sharp
{
    class Program
    {
        static void Main(string[] args)
        {
            RandomDataGenerator generator = new RandomDataGenerator();
            List<UserNode> users = generator.Generate();

            foreach (var user in users)
            {
                Console.WriteLine($"{user} user ID: {user.Id}");
            }

            //Console.WriteLine($"Min distance between 2 and 23: {MinimumDistance.DisplayShortestDistance(users, 2, 23)}");

            ListOfFriendsAtDegree.DisplayListOfFriendsAtDegree(users, 1, 3);

            Console.WriteLine("Done");
            Console.ReadKey();
        }

        
    }
}
