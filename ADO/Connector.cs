using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO
{
    internal class Connector
    {
        SqlConnection connection;
        public Connector(string connection_string)
        {
            connection = new SqlConnection(connection_string);
            //Console.WriteLine(connection.ConnectionString);
        }
        public void Select(string cmd)
        {
            SqlCommand command = new SqlCommand(cmd, connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            int[] string_sizes = new int[reader.FieldCount];
            int interval = 11;
            for (int i = 0; i < reader.FieldCount; i++)
                if (reader.GetName(i).ToString().Length > string_sizes[i])
                    string_sizes[i] = reader.GetName(i).ToString().Length + interval;
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (reader[i].ToString().Length > string_sizes[i])
                        string_sizes[i] = reader[i].ToString().Length + interval;
                }
            }
            reader.Close();

            reader = command.ExecuteReader();
            for (int i = 0; i < reader.FieldCount; i++)
                Console.Write($"{reader.GetName(i).PadRight(string_sizes[i])}");
            Console.WriteLine();
            for (int i = 0; i < string_sizes.Sum(); i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
            while (reader.Read())
            {
                //Console.WriteLine($"{reader[0]}\t{reader[1]}\t{reader[2]}");
                for (int i = 0; i < reader.FieldCount; i++)
                    Console.Write(reader[i].ToString().PadRight(string_sizes[i]));
                Console.WriteLine();
            }
            reader.Close();

            connection.Close();

        }
        public void Select(string fields, string tables, string condition = "")
        {
            string cmd = $"SELECT {fields} FROM {tables}";
            if (condition != "") cmd += $" WHERE {condition}";
            Select(cmd);
        }
        public object Scalar(string cmd)
        {
            object value = null;
            SqlCommand command = new SqlCommand(cmd, connection);
            connection.Open();
            value = command.ExecuteScalar();
            connection.Close();
            connection.Close();
            return value;
        }
        public void Insert(int Director_id, string First_name, string Last_name)//Directors input
        {
            if (!Check_data_in_Table(First_name, Last_name))
            {
                string cmd = ($"INSERT Directors(director_id, first_name, last_name) VALUES ({Director_id}, {First_name}, {Last_name})");
                connection.Open();
                SqlCommand command = new SqlCommand(cmd, connection);
                command.ExecuteNonQuery();
                connection.Close();
                Console.WriteLine("Запись успешна!");
            }
            else
                Console.WriteLine("Такая запись уже есть!");
        }
        public void Insert(int Mouvie_id, string Title, string Release_date, int Director)//Movies input
        {
            if (!Check_data_in_Table(Title, Release_date, Director))
            {
                string cmd = $"INSERT Movies(mouvie_id, title, release_date, director) VALUES ({Mouvie_id}, {Title}, {Release_date}, {Director})";
                connection.Open();
                SqlCommand command = new SqlCommand(cmd, connection);
                command.ExecuteNonQuery();
                connection.Close();
                Console.WriteLine("Запись успешна!");
            }
            else
                Console.WriteLine("Такая запись уже есть!");
        }
        public bool Check_data_in_Table(string first_namE, string last_namE)//Directors check for input
        {
            string cmd = $"SELECT * FROM Directors WHERE (first_name = {first_namE} AND last_name = {last_namE})";
            connection.Open();
            SqlCommand command = new SqlCommand(cmd, connection);
            SqlDataReader reed = command.ExecuteReader();
            int result = 0;
            if (reed.HasRows == true)
                    result += 1;
            Console.WriteLine(result);
            connection.Close();
            if (result > 0)
            {
                result = 0;
                return true;
            }
            else
            {
                result = 0;
                return false;
            }
        }
        public bool Check_data_in_Table(string titlE, string release_datE, int directoR)//Movies check for input
        {
            string cmd = $"SELECT * FROM Movies WHERE (title = {titlE} AND release_date = {release_datE} AND director = {directoR})";
            connection.Open();
            SqlCommand command = new SqlCommand(cmd, connection);
            SqlDataReader reed = command.ExecuteReader();
            int result = 0;
            if (reed.HasRows == true)
                result += 1;
            
            Console.WriteLine(result);
            connection.Close();
            if (result > 0)
            {
                result = 0;
                return true;
            }
            else
            {
                result = 0;               
                return false;
            }
        }
    }
}
