using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hash_таблицы
{
    public class ExternalChainHashTable : IHashTable
    {
        private List<Student>[] table;
        public int Size { get; }

        public ExternalChainHashTable(int size)
        {
            Size = size;
            table = new List<Student>[Size];
        }

        public bool Add(Student student)
        {
            if (Find(student.ID) != null)
            {
                return false;
            }

            int hash = Student.Hash(student.ID);
            int index = hash % Size;

            List<Student> list = table[index];
            if (list == null)
            {
                list = new List<Student>();
                table[index] = list;
            }

            list.Add(student);
            return true;
        }

        public void Clear()
        {
            for (int i = 0; i < Size; i++)
            {
                table[i] = null;
            }
        }

        public void Delete(string id)
        {
            int hash = Student.Hash(id);
            int index = hash % Size;

            List<Student> list = table[index];
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].ID == id)
                {
                    list.RemoveAt(i);
                    return;
                }
            }
        }

        public Student Find(string id)
        {
            int hash = Student.Hash(id);
            int index = hash % Size;

            List<Student> list = table[index];
            if (list == null)
            {
                return null;
            }
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].ID == id)
                {
                    return list[i];
                }
            }

            return null;
        }

        public List<Student> GetData()
        {
            List<Student> result = new List<Student>();    
            foreach (List<Student> list in table)
            {
                if (list != null)
                {
                    foreach (Student student in list)
                    {
                        result.Add(student);
                    }
                }
            }
            return result;
        }
    }
}
