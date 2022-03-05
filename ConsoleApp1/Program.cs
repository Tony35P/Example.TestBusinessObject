using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connString = @"Data Source=.\SQL2019;Initial Catalog=dbBlog;Persist Security Info=True;user id=sa5;password=sa5";
            string categoryName = "Health456";

            var service = new CategoryService(connString);
            service.Create(categoryName);
        }
    }
    public class CategoryService
    {
        private string connString;
        public CategoryService(string connString)
        {
            this.connString = connString;
        }

        public void Create(string categoryName)
        {
            var sql = "SELECT Id FROM Categories WHERE CategoryName=@CategoryName";

            using (var conn = new SqlConnection(connString))
            {
                var result = conn.QueryFirstOrDefault(sql, new { CategoryName = categoryName });
                if (result != null)
                {
                    throw new Exception("相同的分組名稱已存在");
                }

                sql = "INSERT INTO Categories(CategoryName)VALUES(@CategoryName)";
                conn.Execute(sql, new { CategoryName = categoryName });
            }
        }
    }
}
