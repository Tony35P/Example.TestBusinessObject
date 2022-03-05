using NUnit.Framework;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tony.Blog.Services;
using NSubstitute;

namespace Tony.Service.Test
{
    [TestFixture]
    public class CategoryServiceTest
    {
        [Test]
        public void Create_傳入不存在的名稱_順利建檔()
        {
            string connString = string.Empty;
            ICategoryDAO dao = NSubstitute.Substitute.For<ICategoryDAO>(); //產生一個假的DAO物件
            //dao.IsExist("Healthaaa").Returns(false); // 傳回false Create繼續執行; 傳回true 代表名稱已經存在, 便會報錯
            dao.IsExist(Arg.Any<string>()).Returns(false); // 更進一步, 無論傳入甚麼字串都return false

            string categoryName = "Healthaaa";

            var service = new CategoryService(dao);
            service.Create(categoryName);

            // 這裡會碰到一個問題: 無法寫Assert()
            // 因為service.Create() 是 void, 所以默默地就做完了
            dao.Received().Create(categoryName); // 在Nsub可以做到, 這裡斷言有呼叫dao.Create()
        }

        [Test]
        public void Create_傳入已存在的名稱_丟出異常()
        {
            ICategoryDAO dao = NSubstitute.Substitute.For<ICategoryDAO>(); 
            dao.IsExist(Arg.Any<string>()).Returns(true); 

            string categoryName = "Healthaaa";

            var service = new CategoryService(dao);
            var ex = Assert.Throws<Exception>(() => service.Create(categoryName));

            Assert.That(ex.Message, Does.Contain("存在")); 
            //Assert.AreEqual("相同的名稱已存在",ex.Message);
            //測試時不要很多東西把寫死, 因為錯誤訊息可能更動, 而其內容也不太重要
        }
    }
}
