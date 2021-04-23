using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;

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
        public static bool IfWasEdited;
        private static List<GroupMember> GroupMembers = new List<GroupMember>();
        private static List<(string, string)> GroupMembers_str = new List<(string, string)>();

        private static bool IsExist((string, string) member)
        {

            foreach (var memb in GroupMembers_str)
            {
                if (member.Item2 == memb.Item2) return true;
            }

           return false;
        }
               
        public void AddMember(IPAddress ip, string name)
        {

            (string, string) member_str = (name, ip.ToString());
            GroupMember member = new GroupMember(ip, name);

            if (!IsExist(member_str))
            {
                IfWasEdited = true;
                GroupMembers.Add(member);
                GroupMembers_str.Add(member_str);
                //if(onAddIpAdress!=null)

                OnAddIpAdress?.Invoke(this,ReturnGroupList());//Вызываем событие если что-то добавилось в лист
            }
        }
        public delegate void MethodContainer(object sender, List<(string, string)> lst);
        public event MethodContainer OnAddIpAdress;

        public static List<(string, string)> ReturnGroupList()
        {
            IfWasEdited = false;
            return GroupMembers_str;
        }
    }
}
