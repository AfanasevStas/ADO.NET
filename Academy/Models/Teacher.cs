using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Models
{
    internal class Teacher:Human
    {
        internal int teacher_id;
        internal string work_since;
        internal decimal rate;
        public Teacher(int id, string last_name, string first_name, string middle_name, string birth_date, string email, string phone, Image photo, int group,string work_since, decimal rate) : base(id, last_name, first_name, middle_name, birth_date, email, phone, photo)
        {
            this.work_since = work_since;
            this.rate = rate;
        }
        public Teacher(Human human, string work_since, decimal rate) : base(human)
        {
            this.work_since = work_since;
            this.rate = rate;
        }
        public Teacher(object[] values) : base(values)
        {
            this.work_since = values[8].ToString();
            this.rate = Convert.ToDecimal(values[9]);
            this.teacher_id = Convert.ToInt32(values[0]);
        }

        public override string GetNames()//MY CODE
        {
            return base.GetNames()+",work_since,rate,teacher_id";
        }
        public override string GetValues()//MY CODE
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["PV_522_Import"].ConnectionString;
            string cmd = "SELECT * FROM Teachers;";
            SqlConnection connect = new SqlConnection(connectionstring);
            SqlCommand command = new SqlCommand(cmd, connect);
            connect.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
                teacher_id = Convert.ToInt32(reader[0]);
            connect.Close();

            DateTime d = DateTime.Parse(work_since);
            string result = d.ToString("yyyy-MM-dd");
            return base.GetValues()+$",N'{result}',{rate},{teacher_id+1}";
        }
        public string GetUpdateString(string A)//MY CODE
        {
            DateTime d = DateTime.Parse(work_since);
            string result = d.ToString("yyyy-MM-dd");
            return base.GetUpdateString()+$",work_since=N'{result}',rate={A}";
        }
    }
}
