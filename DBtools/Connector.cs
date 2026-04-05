using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace DBtools
{
    public class Connector
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
        public string GetPrimaryKeyColumnName(string table)
        {
            string cmd = $@"SELECT	COLUMN_NAME FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE
                         WHERE	CONSTRAINT_NAME=
                         (
                         SELECT	CONSTRAINT_NAME 
                         FROM	INFORMATION_SCHEMA.TABLE_CONSTRAINTS 
                         WHERE	TABLE_NAME=N'{table}'
                         AND		CONSTRAINT_TYPE=N'PRIMARY KEY'
                         );";
            return Scalar(cmd).ToString();
        }
        public int GetLastPrimaryKey(string table)
        {
            return (int)Scalar($"SELECT MAX({GetPrimaryKeyColumnName(table)}) FROM {table}");
        }
        public int GetNextPrimaryKey(string table)
        {
            return GetLastPrimaryKey(table) + 1;
        }
        public void Insert(string cmd)
        {
            SqlCommand command = new SqlCommand(cmd, connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void Insert(string table, string fields, string values)
        {
            string[] split_fields = fields.Split(',');
            string[] split_values = values.Split(',');
            if (split_fields.Length != split_values.Length) return;
            string condition = "";
            for (int i = 1; i < split_fields.Length; i++)
            {
                condition += $"{split_fields[i]} = {split_values[i]}";
                if (i != split_values.Length - 1) condition += " AND ";
            }
            if (Scalar($"SELECT {GetPrimaryKeyColumnName(table)} FROM {table} WHERE {condition} ") != null) return;
            Insert($"INSERT {table}({fields}) VALUES({values})");
        }
        public void Update(string Old_First_name, string Old_Last_name, string First_name, string Last_name)//Update to Directors Table
        {
            string cmd = $"UPDATE Directors SET first_name = N'{First_name}', last_name = N'{Last_name}' WHERE first_name = N'{Old_First_name}' AND last_name = N'{Last_name}'";
            connection.Open();
            SqlCommand command = new SqlCommand(cmd, connection); 
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void Update(string Old_Title, string Old_Release_date, int Old_Director, string Title, string Release_date, int Director)//Update to Movies Table
        {
            string cmd = $"UPDATE Movies SET title = N'{Title}', release_date = N'{Release_date}', director = {Director} WHERE title = N'{Old_Title}' AND release_date = N'{Old_Release_date}' AND director = {Old_Director}";
            connection.Open();
            SqlCommand command = new SqlCommand(cmd, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}

