using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hash_таблицы
{
    public class InnerChainHashTable : IHashTable
    {
        private InnerChainTableItem[] table;
        public int Size { get; }
        public InnerChainHashTable(int size)
        {
            Size = size;
            table = new InnerChainTableItem[Size];
        }
        public bool Add(Student student)
        {
            if (Find(student.ID) != null)
            {
                return false;
            }

            int hash = Student.Hash(student.ID);
            int index = hash % Size;

            if (table[index] == null || table[index].IsDeleted)
            {
                table[index] = new InnerChainTableItem { Student = student, IsDeleted = false};
                return true;
            }
            InnerChainTableItem previous = table[index];
            
            while (previous.Next.HasValue)
            {
                index = previous.Next.Value;
                previous = table[previous.Next.Value];
            }
            
            for (int i = index + 1; i < Size; i++)
            {
                
                if (table[i] == null || table[i].IsDeleted)
                {
                    table[i] = new InnerChainTableItem { Student = student, IsDeleted = false };
                    previous.Next = i;
                    return true;
                }
            }

            for (int i = 0; i < index; i++)
            {

                if (table[i] == null || table[i].IsDeleted)
                {
                    table[i] = new InnerChainTableItem { Student = student, IsDeleted = false };
                    previous.Next = i;
                    return true;
                }
            }

            return false;
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

            if (table[index] == null)
            {
                return;
            }

            if (!table[index].IsDeleted && table[index].Student.ID == id)
            {
                table[index].IsDeleted = true;
                return;
            }

            InnerChainTableItem previous = table[index];

            while (previous.Next.HasValue)
            {
                InnerChainTableItem next = table[previous.Next.Value];

                if (next != null && next.Student.ID == id)
                {
                    previous.Next = next.Next;
                    next.IsDeleted = true;
                    return;
                }

                previous = next;
            }
        }

        public Student Find(string id)
        {
            int hash = Student.Hash(id);
            int index = hash % Size;

            if (table[index] == null)
            {
                return null;
            }

            if (!table[index].IsDeleted && table[index].Student.ID == id)
            {
                return table[index].Student;
            }

            InnerChainTableItem tmp = table[index];

            while (tmp.Next.HasValue)
            {
                tmp = table[tmp.Next.Value];
                if (!tmp.IsDeleted && tmp.Student.ID == id)
                {
                    return tmp.Student;
                }
            }

            return null;
        }

        public List<Student> GetData()
        {
            List<Student> list = new List<Student>();
            foreach (InnerChainTableItem tableItem in table)
            {
                if (tableItem != null && !tableItem.IsDeleted)
                {
                    list.Add(tableItem.Student);
                }
            }
            return list;
        }
    }
}
