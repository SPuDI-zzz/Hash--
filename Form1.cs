using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hash_таблицы
{
    public partial class Form1 : Form
    {
        private IHashTable hashTable = new InnerChainHashTable(31);

        public Form1()
        {
            InitializeComponent();
        }

        private void add_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(FormState.Add);
            form2.ShowDialog();
            hashTable.Add(form2.student);
            Redraw();

        }

        private void Redraw() 
        {
            List<Student> students = hashTable.GetData();
            dataGridView1.Rows.Clear();
            if (students.Count > 0)
            {
                dataGridView1.RowCount = students.Count;
                for (int i = 0; i < students.Count; i++)
                {
                    dataGridView1.Rows[i].Cells[0].Value = students[i].ID;
                    dataGridView1.Rows[i].Cells[1].Value = students[i].FIO;
                    dataGridView1.Rows[i].Cells[2].Value = students[i].Course;
                    dataGridView1.Rows[i].Cells[3].Value = students[i].Group;
                }
            }
        }

        private void find_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(FormState.Find);
            form2.ShowDialog();
            string id = form2.student.ID;
            Student student = hashTable.Find(id);
            if(student != null)
            {
                form2 = new Form2(FormState.Display, student);
                form2.ShowDialog();
            }
            else
            {
                MessageBox.Show("Student not found");
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(FormState.Delete);
            form2.ShowDialog();
            string id = form2.student.ID;
            hashTable.Delete(id);
            Redraw();
        }

        private void clear_Click(object sender, EventArgs e)
        {
            hashTable.Clear();
            Redraw();
        }

        private void edit_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(FormState.Find);
            form2.ShowDialog();
            string id = form2.student.ID;
            Student student = hashTable.Find(id);
            if (student == null)
            {
                MessageBox.Show("Student not found");
            }
            else
            {
                form2 = new Form2(FormState.Edit, student);
                form2.ShowDialog();
                hashTable.Delete(id);
                hashTable.Add(form2.student);
                Redraw();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
