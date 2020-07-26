using System.Collections.Generic;
using System.Threading;

namespace BFS_c_sharp.Model
{
    public class UserNode
    {
        static int nextId = -1;
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        private readonly HashSet<UserNode> _friends = new HashSet<UserNode>();

        public HashSet<UserNode> Friends
        {
            get { return _friends; }
        }


        public UserNode() { }

        public UserNode(string firstName, string lastName)
        {
            Id = Interlocked.Increment(ref nextId);
            FirstName = firstName;
            LastName = lastName;
        }

        public void AddFriend(UserNode friend)
        {
            Friends.Add(friend);
            friend.Friends.Add(this);
        }

        public override string ToString()
        {
            return FirstName + " " + LastName + "(" + Friends.Count + ")";
        }
    }
}
