using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Interface
{
    public class GroupMember
    {
        public GroupMember(IPAddress ip, string name)
        {
            IP = ip;
            Name = name;
        }
        public static IPAddress IP;
        public static string Name;
    }
    public class AvailableConection
    {
        public static List<GroupMember> GroupMembers = new List<GroupMember>();
        public static List<string> GroupMembers_str = new List<string>();

        private static bool IsExist(string member)
        {

            foreach (var memb in GroupMembers_str)
            {
                if (member == memb) return true;
            }

           return false;
        }
        public static void AddMember(IPAddress ip, string name)
        {
            string member_str = name + ' ' + ip.ToString();
            GroupMember member = new GroupMember(ip, name);

            if (!IsExist(member_str))
            {
                GroupMembers.Add(member);
                GroupMembers_str.Add(member_str);
            }
        }

        public static List<string> ReturnGroupList()
        {
            return GroupMembers_str;
        }
    }
}
