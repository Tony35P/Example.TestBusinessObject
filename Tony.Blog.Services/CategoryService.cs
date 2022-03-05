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
        private ICategoryDAO dao;
        public CategoryService(ICategoryDAO dao)
        {
            this.dao = dao;
        }

        public void Create(string categoryName)
        {
            if(dao.IsExist(categoryName))
            {
                throw new Exception("相同的名稱已存在!");
            }
            dao.Create(categoryName);
        }
    }
    public interface ICategoryDAO
    {
        void Create(string categoryName);

        bool IsExist(string categoryName);
    }
    public class CategoryDAO : ICategoryDAO
    {
        private string connString;
        public CategoryDAO(string connString)
        {
            this.connString=connString;
        }
        public void Create(string categoryName)
        {
            using (var conn = new SqlConnection(connString))
            {                
                var sql = "INSERT INTO Categories(CategoryName)VALUES(@CategoryName)";
                conn.Execute(sql, new { CategoryName = categoryName });
            }
        }

        public bool IsExist(string categoryName)
        {
            var sql = "SELECT Id FROM Categories WHERE CategoryName=@CategoryName";

            using (var conn = new SqlConnection(connString))
            {
                var result = conn.QueryFirstOrDefault(sql, new { CategoryName = categoryName });
               
                return (result != null);
            }
        }
    }
}
