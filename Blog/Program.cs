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
            //ReadUsers();
            //ReadUser();
            //CreatUser();
            UpdateUser();
        }

        public static void ReadUsers()
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

        public static void ReadUser()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var user = connection.Get<User>(1);
                Console.WriteLine(user.Name);

            }
        }

        public static void CreatUser()
        {
            var createUser = new User()
            {
                Name = "Débora dos bodes",
                Email = "debes@gmail.com",
                PasswordHash = "Hash",
                Bio = "cuidadora de bode",
                Image = "https://",
                Slug = "debes-bode"
            };

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Insert<User>(createUser);
                Console.WriteLine("Cadastro realizado com sucesso!");
            }
        }

        public static void UpdateUser()
        {
            var updateUser = new User()
            {
                Id = 1,
                Name = "Lucas Mota",
                Email = "lulu@gmail.com",
                PasswordHash = "Hash",
                Bio = "Dev c#",
                Image = "https://",
                Slug = "dev-lucas"
            };

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Update<User>(updateUser);
                Console.WriteLine("Usuário atualizado com sucesso!");
            }
        }

        public static void DeleteUser()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var deleteUser = connection.Get<User>(1);
                connection.Delete<User>(deleteUser);
                Console.WriteLine("Usuário deletado!");
            }
        }
    }
}
