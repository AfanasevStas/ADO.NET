using Academy.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Academy
{
    public partial class TeacherForm : HumanForm
    {
        Models.Teacher teacher;
        public TeacherForm()
        {
            InitializeComponent();
        }
        public TeacherForm(int id) : this()
        {
            DataTable table = DataBase.Connector.Load($"SELECT * FROM Teachers WHERE teacher_id={id}");
            teacher = new Models.Teacher(table.Rows[0].ItemArray);
            human = teacher;
            Extract();
        }
        protected override void Extract()//MY CODE
        {
            base.Extract();
            dtpWorkSince.Value = Convert.ToDateTime(teacher.work_since);
            textBoxRate.Text = Convert.ToString(teacher.rate);
        }
        protected override void buttonOK_Click(object sender, EventArgs e)//MY CODE
        {
            base.buttonOK_Click(sender, e);
            teacher = new Models.Teacher(human, Convert.ToString(dtpWorkSince.Value), Convert.ToDecimal(textBoxRate.Text));
            if (teacher.id == 0)
            {
                teacher.id = teacher.teacher_id;
                DataBase.Connector.Scalar($"INSERT Teachers({teacher.GetNames()}) VALUES({teacher.GetValues()});");
            }
            else
            {
                string A = Convert.ToString(teacher.rate);
                A = A.Replace(",", ".");
                DataBase.Connector.Update($"UPDATE Teachers SET {teacher.GetUpdateString(A)} " +
                $"WHERE teacher_id={teacher.id}");
            }
        }
    }
}
