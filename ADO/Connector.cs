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
                SqlCommand command = new SqlCommand(cmd,connection);
                command.ExecuteNonQuery();
                connection.Close();
                Console.WriteLine("Запись успешна!");
            }
            else
                Console.WriteLine("Такая запись уже есть!");
        }
        public bool Check_data_in_Table(string first_namE, string last_namE)//Directors check for input
        {
            string cmd = $"USE Movies_PV_522;\nGO\ndbo.Check_Directors {first_namE},{last_namE}";
            connection.Open();
            SqlCommand command = new SqlCommand(cmd, connection);
            int result = (int)command.ExecuteScalar();
            connection.Close();
            if (result != 0)
                return false;
            else
                return true;
        }
        public bool Check_data_in_Table(string titlE, string release_datE,int directoR)//Movies check for input
        {
            string cmd = $"USE Movies_PV_522;\nGO \ndbo.Check_Movies {titlE},{release_datE},{directoR}";
            connection.Open();
            SqlCommand command = new SqlCommand(cmd, connection);
            int result = (int)command.ExecuteScalar();
            connection.Close();
            if (result != 0)
                return false;
            else
                return true;
        }
        public void Create_Function_on_Server()//Creating a verification function on the server
        {
            string cmd =    "CREATE OR ALTER FUNCTION Check_Directors(@first_name AS NVARCHAR(50), @last_name AS NVARCHAR(50))RETURNS INT" +
                            "\nAS\n" +
                            "BEGIN\n" +
                            "DECLARE @res AS INT;" +
                            "\nSET @res = (SELECT COUNT(director_id) FROM Directors WHERE (first_name = @first_name AND last_name = @last_name));" +
                            "\nRETURN @res" +
                            "\nEND;";
            //--------------------------------------------------------------------------------------------------------------------------------------------------------
            string cmd_2 = "CREATE OR ALTER FUNCTION Check_Movies(@title AS NVARCHAR(50), @release_date AS DATE, @director AS INT)RETURNS INT" +
                            "\nAS\n" +
                            "BEGIN\n" +
                            "DECLARE @res AS INT;" +
                            "\nSET @res = (SELECT COUNT(mouvie_id) FROM Movies WHERE (title = @title AND release_date = @release_date AND director = @director));" +
                            "\nRETURN @res" +
                            "\nEND;";
            //--------------------------------------------------------------------------------------------------------------------------------------------------------
            connection.Open();
            SqlCommand command = new SqlCommand(cmd, connection);
            SqlCommand command_2 = new SqlCommand(cmd_2, connection);
            command.ExecuteNonQuery();
            command_2.ExecuteNonQuery();
            connection.Close();
        }
    }
}
