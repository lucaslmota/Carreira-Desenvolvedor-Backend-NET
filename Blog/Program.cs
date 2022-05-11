using System;
using Blog.Models;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;

namespace Blog
{
    class Program
    {
        private const string ConnectionString = "Server=localhost,1433;Database=Blog;User Id=sa;Password=042393ll;Trusted_Connection=False; TrustServerCertificate=True;";
        static void Main(string[] args)
        {
            ReadUser();
        }

        public static void ReadUser()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var users = connection.GetAll<User>();

                foreach (var user in users)
                {
                    Console.WriteLine(user.Name);
                }
            }
        }
    }
}
