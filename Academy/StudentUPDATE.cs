using Academy.Models;
using DBtools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Academy
{
    public partial class StudentUPDATE : Form//MY CODE
    {
        public StudentUPDATE()
        {
            InitializeComponent();
        }
        int stud_id;
        public StudentUPDATE(
                                 int stud_id,string last_name,
                                 string first_name,string middle_name,
                                 string birth_date,string group
                            )
        {
            InitializeComponent();
            textBoxLastName.Text = last_name;       textBoxFirstName.Text = first_name;
            textBoxMiddleName.Text = middle_name;   dtpBirthDate.Text = birth_date;
            this.stud_id = stud_id;
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["PV_522_Import"].ConnectionString);
            string cmd = $"UPDATE Students SET " +
                            $"last_name=N'{textBoxLastName.Text}'," +
                            $"first_name=N'{textBoxFirstName.Text}'," +
                            $"middle_name=N'{textBoxMiddleName.Text}'," +
                            $"birth_date=TRY_PARSE(N'{dtpBirthDate.Text}' AS DATE USING 'ru-RU')," +
                            $"email=N'{textBoxEmail.Text}'," +
                            $"phone=N'{textBoxPhone.Text}' " +
                            $"WHERE stud_id={Convert.ToInt32(stud_id)}";
            SqlCommand com = new SqlCommand(cmd, connection);
            connection.Open();
            com.ExecuteNonQuery();
            connection.Close();
        }
    }
}
