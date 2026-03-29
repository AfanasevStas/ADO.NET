using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;

namespace ADO
{
    internal class Comands_SELECT
    {
        public static SqlConnection connection;
        public string connection_string = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Movies_PV_522;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static void Select(string cmd)
        {
            SqlCommand command = new SqlCommand(cmd, connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            for(int i = 0; i < reader.FieldCount; i++)
                Console.Write($"{reader.GetName(i)}");
            Console.WriteLine();
            while (reader.Read())
            {
                Console.WriteLine($"{reader[0]}\t{reader[1]}\t{reader[2]}");
            }
            reader.Close();

            connection.Close();
        }
        public static object Scalar(string cmd)
        {
            object value = null;
            SqlCommand command = new SqlCommand(cmd, connection);
            connection.Open();
            value = command.ExecuteScalar();
            connection.Close();
            connection.Close();
            return value;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Comands_SELECT SELECT_1 = new Comands_SELECT();
            Console.WriteLine(SELECT_1.connection_string);
            Comands_SELECT.connection = new SqlConnection(SELECT_1.connection_string);
            string cmd = "SELECT * FROM Directors";

            Comands_SELECT.Select(cmd);
            Console.WriteLine($"Количество записей: {Comands_SELECT.Scalar("SELECT COUNT(*) FROM Directors")}");
            Comands_SELECT.Select("SELECT * FROM Movies");
        }
    }
}
