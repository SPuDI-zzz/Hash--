using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hash_таблицы
{
    public class Student
    {
        public string ID { get; set; }
        public string FIO { get; set; }
        public int Course { get; set; }
        public int Group { get; set; }

        //Проверяет равны ли объект obj с экземляром класса 
        public override bool Equals(object obj)
        {
            return obj is Student student &&
                   ID == student.ID;
        }

        //вычисление значения Хеш-функции для студента
        public static int Hash(string id)
        {
            //проверка на пустую строку
            if (string.IsNullOrWhiteSpace(id))
            {
                return 0;
            }

            //вычисление хеш-функции путем суммирования кодов символов в строке 
            int result = 0;
            foreach(char c in id)
            {
                result += c;
            }
            return result;
        }
    }
}
