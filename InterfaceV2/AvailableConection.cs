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
        private static List<string> GroupMembers_str = new List<string>();

        private static bool IsExist(string member)
        {

            foreach (var memb in GroupMembers_str)
            {
                if (member == memb) return true;
            }

           return false;
        }
               
        public void AddMember(IPAddress ip, string name)
        {
            
            string member_str = name + ' ' + ip.ToString();
            GroupMember member = new GroupMember(ip, name);

            if (!IsExist(member_str))
            {
                IfWasEdited = true;
                GroupMembers.Add(member);
                GroupMembers_str.Add(member_str);
                if(onAddIpAdress!=null)
                onAddIpAdress(this,ReturnGroupList());//Вызываем событие если что-то добавилось в лист
            }
        }
        public delegate void MethodContainer(object sender, List<string> lst);
        public event MethodContainer onAddIpAdress;

        public static List<string> ReturnGroupList()
        {
            IfWasEdited = false;
            return GroupMembers_str;
        }
    }
}
