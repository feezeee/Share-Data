using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share_Data
{
    class getting_name
    {
        public static string get_name()
        {
            string name;

            try 
            {
                name = System.Net.Dns.GetHostName();

            }
            catch 
            {
                //Создание объекта для генерации чисел
                Random rnd = new Random();

                //Получить случайное число (в диапазоне от 0 до 10)
                int value = rnd.Next(100000000, 999999999);
                try
                {
                    name = System.Environment.UserName + value.ToString();

                }
                catch
                {
                    name = "User" + value.ToString();
                }
            }

            return name;
        }
    }
}
