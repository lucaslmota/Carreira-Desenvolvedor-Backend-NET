using System;
using Blog.Models;
using Blog.Repositories;
using Dapper.Contrib.Extensions;
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
            // ReadUsers(connection);
            // ReadRoles(connection);

            ReadUserWithRoles(connection);

            connection.Close();
        }

        public static void ReadUsers(SqlConnection connection)
        {

            var repository = new Repository<User>(connection);
            var users = repository.GetAll();

            foreach (var user in users)
            {
                Console.WriteLine(user.Name);

                foreach (var role in user.Roles)
                {
                    Console.WriteLine($"- {role.Name}");
                }
            }
        }

        public static void ReadUserWithRoles(SqlConnection connection)
        {
            var repository = new UserRepository(connection);
            var users = repository.GetWithRoles();

            foreach (var user in users)
            {
                Console.WriteLine(user.Name);
                foreach (var role in user.Roles)
                {
                    Console.WriteLine($" - {role.Name}");
                }
            }
        }

        public static void ReadRoles(SqlConnection connection)
        {
            var repository = new Repository<Role>(connection);
            var roles = repository.GetAll();

            foreach (var role in roles)
            {
                Console.WriteLine(role.Name);
            }
        }

        public static void ReadTag(SqlConnection connection)
        {
            var repository = new Repository<Tag>(connection);
            var tags = repository.GetAll();

            foreach (var tag in tags)
            {
                Console.WriteLine(tag.Name);
            }
        }

    }
}
