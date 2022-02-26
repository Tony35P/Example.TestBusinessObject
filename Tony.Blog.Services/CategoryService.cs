using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Tony.Blog.Services
{
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
                var result = conn.QueryFirstOrDefault(sql, new {CategoryName=categoryName});
                if (result != null)
                {
                    throw new Exception("相同的分組名稱已存在");
                }

                sql = "INSERT INTO Categories(CategoryName)VALUES(@CategoryName)";
                conn.Execute(sql, new {CategoryName=categoryName});
            }
        }
    }
}
