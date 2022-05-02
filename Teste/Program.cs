using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Data.SqlClient;
using Teste.Models;

namespace Teste
{
    class Program
    {
        static void Main(string[] args)
        {
            const string ConnectionString = "Server=localhost,1433;Database=balta;User Id=sa;Password=042393ll;Trusted_Connection=False; TrustServerCertificate=True;";

            using (var connection = new SqlConnection(ConnectionString))
            {
                //CreatCategory(connection);
                //UpdateCategory(connection);
                //DeleteCategory(connection);
                //CreaManyCategory(connection);
                //ExecuteProcedure(connection);
                //ExecutereadProcedure(connection);
                //ExecuteScalar(connection);
                //Aqui era pra ter uma view, mas ela é igaul um select
                //OneToOne(connection);
                //OneToMany(connection);
                //QueryMultiple(connection);
                //SelectIn(connection);
                //Like(connection);
                //Transaction(connection);
                //ListCategories(connection);


            }
        }

        static void ListCategories(SqlConnection connection)
        {
            var categories = connection.Query<Category>("SELECT [Id], [Title] FROM [Category]");
            foreach (var item in categories)
            {
                Console.WriteLine($"{item.Id} - {item.Title}");
            }
        }

        static void CreatCategory(SqlConnection connection)
        {
            var category = new Category();
            category.Id = Guid.NewGuid();
            category.Title = "Amazon AWS";
            category.Url = "amazon";
            category.Summary = "AWS cloud";
            category.Order = 8;
            category.Description = "Categoria destinada a testes";
            category.Featured = false;

            //SQL Injection -> nunca fazer um insert ou update concatenando string

            var insertSql = @"INSERT INTO 
                [Category] 
                VALUES(
                    @Id, 
                    @Title, 
                    @Url, 
                    @Summary, 
                    @Order, 
                    @Description, 
                    @Featured)";


            var rows = connection.Execute(insertSql, new
            {
                category.Id,
                category.Title,
                category.Url,
                category.Summary,
                category.Order,
                category.Description,
                category.Featured
            });

            Console.WriteLine($"{rows} linhas inseridas");
        }

        static void CreaManyCategory(SqlConnection connection)
        {
            var category = new Category();
            category.Id = Guid.NewGuid();
            category.Title = "Amazon AWS";
            category.Url = "amazon";
            category.Summary = "AWS cloud";
            category.Order = 8;
            category.Description = "Categoria destinada a testes";
            category.Featured = false;

            var category2 = new Category();
            category2.Id = Guid.NewGuid();
            category2.Title = "Nova categorya";
            category2.Url = "categoria-nova";
            category2.Summary = "nova categoria";
            category2.Order = 9;
            category2.Description = "Nova Categoria destinada a testes";
            category2.Featured = true;
            //SQL Injection -> nunca fazer um insert ou update concatenando string

            var insertSql = @"INSERT INTO 
                [Category] 
                VALUES(
                    @Id, 
                    @Title, 
                    @Url, 
                    @Summary, 
                    @Order, 
                    @Description, 
                    @Featured)";


            var rows = connection.Execute(insertSql, new[]
            {
                new
                {
                category.Id,
                category.Title,
                category.Url,
                category.Summary,
                category.Order,
                category.Description,
                category.Featured
                },

                new
                {
                category2.Id,
                category2.Title,
                category2.Url,
                category2.Summary,
                category2.Order,
                category2.Description,
                category2.Featured
                }
            }

            );

            Console.WriteLine($"{rows} linhas inseridas");
        }
        static void UpdateCategory(SqlConnection connection)
        {
            var updateQuery = "UPDATE [Category] SET [Title]=@title WHERE [Id]=@id";
            var rows = connection.Execute(updateQuery, new
            {
                id = new Guid("af3407aa-11ae-4621-a2ef-2028b85507c4"),
                title = "Frontend 2022"
            });

            Console.WriteLine($"{rows} registros atualizadas");
        }

        static void DeleteCategory(SqlConnection connection)
        {
            var deleteQuery = "DELETE FROM [Category] WHERE [Id]=@id";
            var colunasAfetadas = connection.Execute(deleteQuery, new
            {
                id = new Guid("76e17208-5027-4d7a-8312-12ea4a1fb508")
            });

            Console.WriteLine($"Deleted Rows: {colunasAfetadas}");
        }

        static void ExecuteProcedure(SqlConnection connection)
        {
            var procedure = "spDeleteStudent";
            var pars = new { StudentId = "8c02a520-0aa9-4ada-b82c-2da1cdff0e9d" };
            var affectedRows = connection.Execute(
                procedure,
                pars,
                commandType: CommandType.StoredProcedure
            );

            Console.WriteLine($"{affectedRows} linhas afetadas");
        }
        static void ExecutereadProcedure(SqlConnection connection)
        {
            var procedure = "spGetCoursesByCategory";
            var pars = new { CategoryId = "af3407aa-11ae-4621-a2ef-2028b85507c4" };
            var courses = connection.Query(
                procedure,
                pars,
                commandType: CommandType.StoredProcedure);
            foreach (var item in courses)
            {
                Console.WriteLine(item.Id);
            }
        }

        static void ExecuteScalar(SqlConnection connection)
        {
            var category = new Category();
            category.Title = "Aprendendo Execute Escalar";
            category.Url = "dapper";
            category.Summary = "Execute escalar dapper";
            category.Order = 10;
            category.Description = "Categoria destinada a testes";
            category.Featured = false;

            //SQL Injection -> nunca fazer um insert ou update concatenando string

            var insertSql = @"
            INSERT INTO 
                [Category]
            OUTPUT inserted.[Id] 
                VALUES(
                    NEWID(), 
                    @Title, 
                    @Url, 
                    @Summary, 
                    @Order, 
                    @Description, 
                    @Featured)";


            var id = connection.ExecuteScalar<Guid>(insertSql, new
            {
                category.Title,
                category.Url,
                category.Summary,
                category.Order,
                category.Description,
                category.Featured
            });

            Console.WriteLine($"O Id inserido é: {id}");
        }

        static void OneToOne(SqlConnection connection)
        {
            var sql = @"
                SELECT
                    *
                FROM
                    [CareerItem]
                INNER JOIN
                    [Course] ON [CareerItem].[CourseId] = [Course].Id";

            var items = connection.Query<CareerItem, Course, CareerItem>(
                sql,
                (careerItem, course) =>
                {
                    careerItem.Course = course;
                    return careerItem;
                }, splitOn: "Id");

            foreach (var item in items)
            {
                Console.WriteLine($"{item.Title} - Course: {item.Course.Title}");
            }
        }

        static void OneToMany(SqlConnection connection)
        {
            var sql = @"
                SELECT
                    [Career].[Id],
                    [Career].[Title],
                    [CareerItem].[CareerId],
                    [CareerItem].[Title]
                FROM
                    [Career]
                INNER JOIN
                    [CareerItem] ON [CareerItem].[CareerId] = [Career].Id
                ORDER BY
                    [Career].[Title]";

            var careers = new List<Career>();
            var items = connection.Query<Career, CareerItem, Career>(
                sql,
                (career, careerItem) =>
                {
                    var car = careers.Where(x => x.Id == career.Id).FirstOrDefault();
                    if (car == null)
                    {
                        car = career;
                        car.Items.Add(careerItem);
                        careers.Add(car);
                    }
                    else
                    {
                        car.Items.Add(careerItem);
                    }
                    return career;
                }, splitOn: "CareerId");

            foreach (var career in careers)
            {
                Console.WriteLine($"{career.Title}");

                foreach (var item in career.Items)
                {
                    Console.WriteLine($"- {item.Title}");
                }
            }
        }

        static void QueryMultiple(SqlConnection connection)
        {
            var query = "SELECT * FROM [Category]; SELECT * FROM [Course]";

            using (var multi = connection.QueryMultiple(query))
            {
                var categories = multi.Read<Category>();
                var courses = multi.Read<Course>();

                foreach (var item in categories)
                {
                    Console.WriteLine($"Categorias: {item.Title}");
                }

                foreach (var item in courses)
                {
                    Console.WriteLine($"Cursos: {item.Title}");
                }
            }
        }

        static void SelectIn(SqlConnection connection)
        {
            var query = @"
            SELECT 
                * 
            FROM 
                [Career] WHERE [Id] IN @Id";

            var items = connection.Query<Career>(query, new
            {
                Id = new[]{
                    "01ae8a85-b4e8-4194-a0f1-1c6190af54cb",
                    "e6730d1c-6870-4df3-ae68-438624e04c72"
                }
            });

            foreach (var item in items)
            {
                Console.WriteLine(item.Title);
            }
        }

        static void Like(SqlConnection connection)
        {
            var query = @"
            SELECT 
                * 
            FROM 
                [Course] WHERE [Title] Like @expre";

            var items = connection.Query<Course>(query, new
            {
                expre = "%.net%"
            });

            foreach (var item in items)
            {
                Console.WriteLine(item.Title);
            }
        }

        static void Transaction(SqlConnection connection)
        {
            var category = new Category();
            category.Id = Guid.NewGuid();
            category.Title = "Testando transcations";
            category.Url = "amazon";
            category.Summary = "AWS cloud";
            category.Order = 8;
            category.Description = "Categoria destinada a testes";
            category.Featured = false;

            //SQL Injection -> nunca fazer um insert ou update concatenando string

            var insertSql = @"INSERT INTO 
                [Category] 
                VALUES(
                    @Id, 
                    @Title, 
                    @Url, 
                    @Summary, 
                    @Order, 
                    @Description, 
                    @Featured)";

            connection.Open();
            using (var transacation = connection.BeginTransaction())
            {
                var rows = connection.Execute(insertSql, new
                {
                    category.Id,
                    category.Title,
                    category.Url,
                    category.Summary,
                    category.Order,
                    category.Description,
                    category.Featured
                }, transacation);

                //transacation.Commit();
                transacation.Rollback();

                Console.WriteLine($"{rows} linhas inseridas");
            }
        }
    }
}
