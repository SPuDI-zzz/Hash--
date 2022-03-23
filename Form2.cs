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
    public partial class Form2 : Form
    {
        private FormState formState;
        public Student student { get; } = new Student();
        public Form2(FormState formState, Student student = null)
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MinimizeBox = false;
            MaximizeBox = false;
            this.formState = formState;
            switch (formState)
            {
                case FormState.Add:
                    return;
                case FormState.Delete:
                case FormState.Find:
                    fio.ReadOnly = true;
                    course.ReadOnly = true;
                    group.ReadOnly = true;
                    Text = "Find student";
                    break;                
                case FormState.Edit:
                    id.ReadOnly = true;
                    id.Text = student.ID;
                    fio.Text = student.FIO;
                    course.Text = student.Course.ToString();
                    group.Text = student.Group.ToString();
                    Text = "Edit student";
                    break;
                case FormState.Display:
                    id.ReadOnly = true;
                    fio.ReadOnly = true;
                    course.ReadOnly = true;
                    group.ReadOnly = true;
                    id.Text = student.ID;
                    fio.Text = student.FIO;
                    course.Text = student.Course.ToString();
                    group.Text = student.Group.ToString();
                    Text = "Display student";
                    break;
                default:
                    break;
            }

        }

        private void ok_Click(object sender, EventArgs e)
        {
            student.ID = id.Text;
            if (formState == FormState.Edit || formState == FormState.Add)
            {
                student.FIO = fio.Text;
                student.Course = Convert.ToInt32(course.Text);
                student.Group = Convert.ToInt32(group.Text);
            }
            Close();
        }
    }
}
