using MySql.Data.MySqlClient;
using MySqlTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Test1();

            //var item = Test2();
            //Console.WriteLine($"name={item.Name},age={item.Age}");
            //Console.Read();

            var items = Test3();
            items.ForEach(item =>
            {
                Console.WriteLine($"name={item.Name},age={item.Age}");
            });
            Console.Read();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        static void Test1()
        {
            using (MySqlConnection conn = new MySqlConnection("server=127.0.0.1;port=3306;database=mytest;uid=root;password=9o0p-[=]"))
            {
                using (var context = new MysqlContext(conn, false))
                {
                    context.Database.CreateIfNotExists();
                    conn.Open();
                    var student = new Student()
                    {
                        Id = Guid.NewGuid(),
                        Name = "小红",
                        Age = 18
                    };
                    context.Students.Add(student);
                    context.SaveChanges();
                }
            }
        }

        //通过sql语句获取model,不需要model和返回列完全一致
        //例如,下面的StudentOther类与Student相比少了id字段,但是都了other字段
        static StudentOther Test2()
        {
            StudentOther item = null;
            using (MySqlConnection conn = new MySqlConnection("server=127.0.0.1;port=3306;database=mytest;uid=root;password=9o0p-[=]"))
            {
                using (var context = new MysqlContext(conn, false))
                {
                    conn.Open();
                    item = context.Database.SqlQuery<StudentOther>("SELECT * FROM mytest.students;").FirstOrDefault();
                }
            }
            return item;
        }

        static List<StudentOther> Test3()
        {
            List<StudentOther> item = null;
            using (MySqlConnection conn = new MySqlConnection("server=127.0.0.1;port=3306;database=mytest;uid=root;password=9o0p-[=]"))
            {
                using (var context = new MysqlContext(conn, false))
                {
                    conn.Open();
                    item = context.Database.SqlQuery<StudentOther>("SELECT * FROM mytest.students;").ToList();
                }
            }
            return item;
        }
    }
}
