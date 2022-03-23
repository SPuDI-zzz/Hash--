using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hash_таблицы
{
    //Хеш-таблица с линейный опробированием
    public class LineHashTable : IHashTable
    {

        private TableItem[] table;
        public int Size { get; }

        private int step;

        public LineHashTable(int size, int step)
        {
            Size = size;
            table = new TableItem[Size];
            this.step = step;
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

            for (int i = index + step; i < Size; i += step)
            {
                if (table[i] == null)
                {
                    return null;
                }
                if (!table[i].IsDeleted && table[i].Student.ID == id)
                {                    
                    return table[i].Student;
                }
            }

            for (int i = 0; i < index; i += step)
            {
                if (table[i] == null)
                {
                    return null;
                } 
                if (table[i] != null && !table[i].IsDeleted && table[i].Student.ID == id)
                {
                    return table[i].Student;
                }
            }

            return null;
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
                table[index] = new TableItem{Student = student, IsDeleted = false};
                return true;
            }

            for (int i = index + step; i < Size; i+=step)
            {
                if (table[i] == null || table[i].IsDeleted)
                {
                    table[i] = new TableItem { Student = student, IsDeleted = false };
                    return true;
                }
            }

            for (int i = 0; i < index; i += step)
            {
                if (table[i] == null || table[i].IsDeleted)
                {
                    table[i] = new TableItem { Student = student, IsDeleted = false };
                    return true;
                }
            }

            return false;
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

            for (int i = index + step; i < Size; i += step)
            {
                if (table[i] == null)
                {
                    return;
                }
                if (!table[i].IsDeleted && table[i].Student.ID == id)
                {
                    table[i].IsDeleted = true;
                    return;
                }
            }

            for (int i = 0; i < index; i += step)
            {
                if (table[i] == null)
                {
                    return;
                }
                if (!table[i].IsDeleted && table[i].Student.ID == id)
                {
                    table[i].IsDeleted = true;
                    return;
                }
            }
        }

        public void Clear()
        {
            for (int i = 0; i < Size; i++)
            {
                table[i] = null;
            }
        }

        public List<Student> GetData()
        {
            List<Student> list = new List<Student>();
            foreach (TableItem tableItem in table) 
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
