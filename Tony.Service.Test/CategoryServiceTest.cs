using NUnit.Framework;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tony.Blog.Services;

namespace Tony.Service.Test
{
    [TestFixture]
    public class CategoryServiceTest
    {
        [Test]
        public void Create_WhenCalled()
        {
            string connString = @"Data Source=.\SQL2019;Initial Catalog=dbBlog;Persist Security Info=True;user id=sa5;password=sa5";
            string categoryName = "Healthaaaaa";

            var service = new CategoryService(connString);
            service.Create(categoryName);
        }
    }
}
