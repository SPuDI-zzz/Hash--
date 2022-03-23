using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hash_таблицы
{
    //Интерфейс хеш-таблицы, содержащий объявление методов, применимых ко всем хеш-таблицам вне зависимости от реализации
    public interface IHashTable
    {
        bool Add(Student student);
        Student Find(string id);
        void Delete(string id);
        List<Student> GetData();
        void Clear();

    }
}
