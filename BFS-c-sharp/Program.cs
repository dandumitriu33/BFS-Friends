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

            Console.WriteLine($"Source user's ID: 2 {users[2].FirstName} {users[2].LastName} and has {(users.Find(u => u.Id == 2)).Friends.Count} friends");
            foreach (var item in users[2].Friends)
            {
                Console.WriteLine($"Friend ID: {item.Id} {item.FirstName} {item.LastName}");
            }
            Console.WriteLine($"User 7 FName: {users[7].FirstName} and LName: {users[7].LastName}");
            Console.WriteLine($"Destination user's ID: 7 {users[7].FirstName} {users[7].LastName} and has {(users.Find(u => u.Id == 7)).Friends.Count} friends");
            foreach (var item in users[7].Friends)
            {
                Console.WriteLine($"Friend ID: {item.Id} {item.FirstName} {item.LastName}");
            }

            Console.WriteLine($"Min distance between 2 and 7: {MinimumDistance.DisplayShortestDistance(users, 2, 7)}");

            Console.WriteLine("Done");
            Console.ReadKey();
        }
    }
}
