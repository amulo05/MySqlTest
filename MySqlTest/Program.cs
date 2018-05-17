using MySql.Data.MySqlClient;
using MySqlTest.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Test6();

            //var item = Test2();
            //Console.WriteLine($"name={item.Name},age={item.Age}");
            Console.Read();

        }

        /// <summary>
        /// 初始化
        /// </summary>
        static void Test1()
        {
            //通过connection打开数据库
            using (MySqlConnection conn = new MySqlConnection("server=101.132.184.123;port=3306;database=mytest;uid=root;password=1q2w3e4r!@;"))
            {
                using (var context = new Db(conn, false))
                {
                    context.Database.CreateIfNotExists();
                    conn.Open();
                    //var student = new Student()
                    //{
                    //    Id = Guid.NewGuid(),
                    //    Name = "小白",
                    //    Age = 18
                    //};
                    //student.Save();

                    //var subject = new Subject()
                    //{
                    //    Id = Guid.NewGuid(),
                    //    Name = "数学",
                    //};
                    //subject.Save();
                    var score = new Score()
                    {
                        StudentId = Guid.Parse("055946c1-29b3-40b1-9170-6496560402e8"),
                        SubjectId = Guid.Parse("0fe8a650-6f58-4619-9f70-8af0785e1dbd"),
                        Value = 99
                    };
                    score.Save();
                }
            }
        }

        //通过Database.SetInitializer使用数据库
        static void Test2()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<Db>());
            using (var context = new Db())
            {
                var student = new Student()
                {
                    Id = Guid.NewGuid(),
                    Name = "小蓝",
                    Age = 25
                };
                student.Save();
            }
        }

        //通过sql语句获取model,不需要model和返回列完全一致
        //例如,下面的StudentOther类与Student相比少了id字段,但是都了other字段
        static StudentOther Test3()
        {
            StudentOther item = null;
            Database.SetInitializer(new CreateDatabaseIfNotExists<Db>());
            using (var context = new Db())
            {

                item = context.Database.SqlQuery<StudentOther>("SELECT * FROM students;").FirstOrDefault();
            }

            return item;
        }

        //测试 FindById 和 update 方法 
        static void Test4()
        {
            using (var context = new Db())
            {
                var id = Guid.Parse("0f89ef6e-d090-4303-b907-df7e144f50f0");
                var item = Student.Dao.FindById(new object[] { id });
                Console.WriteLine($"name={item.Name},age={item.Age}");

                item.Age = item.Age + 1;
                item.Update();
                item = Student.Dao.FindById(new object[] { id });
                Console.WriteLine($"name={item.Name},age={item.Age}");


                //student.Save();
            }
        }

        //通过sql语句获取model,不需要model和返回列完全一致
        //例如,下面的StudentOther类与Student相比少了id字段,但是都了other字段
        static void Test5()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<Db>());
            using (var context = new Db())
            {
                var list = context.Database.SqlQuery<StudentOther>("SELECT * FROM student;").ToList();
                list.ForEach(x => x.Age = x.Age + 1);
                Db.BatchUpdate(list, list.Count());
            }
        }

        //通过FindById获取双主键数据
        //注意：顺序需要和数据表的key顺序一致
        static void Test6()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<Db>());
            var studentId = Guid.Parse("055946c1-29b3-40b1-9170-6496560402e8");
            var subjectId = Guid.Parse("0fe8a650-6f58-4619-9f70-8af0785e1dbd");
            using (var context = new Db())
            {
                var item  = Score.Dao.FindById(new object[] { studentId, subjectId });
                Console.WriteLine($"value={item.Value}");
            }
        }
    }
}
