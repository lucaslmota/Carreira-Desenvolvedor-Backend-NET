using System;
using Blog.Models;
using Blog.Repositories;
using Microsoft.Data.SqlClient;

namespace Blog
{
    class Program
    {
        private const string ConnectionString = "Server=localhost,1433;Database=Blog;User Id=sa;Password=042393ll;Trusted_Connection=False; TrustServerCertificate=True;";
        static void Main(string[] args)
        {
            var connection = new SqlConnection(ConnectionString);
            connection.Open();

            Console.ReadKey();
            connection.Close();

        }



    }
}
