using MySql.Data.Entity;
using MySqlTest.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MySqlTest
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class Db : DbContext
    {
        public static readonly Db Dao = new Db();
        //public DbSet<Student> Students { get; set; }

        public Db()
            : base("Mysql")
        {

        }

        public Db(DbConnection existingConnection, bool contextOwnsConnection)
             : base(existingConnection, contextOwnsConnection)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Student>().MapToStoredProcedures();

            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => !String.IsNullOrEmpty(type.Namespace))
            .Where(type => type.BaseType != null && type.BaseType.IsGenericType &&
                type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Get DbSet
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <returns>DbSet</returns>
        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        /// <summary>
        /// Creates a raw SQL query that will return elements of the given generic type.  The type can be any type that has properties that match the names of the columns returned from the query, or can be a simple primitive type. The type does not have to be an entity type. The results of this query are never tracked by the context even if the type of object returned is an entity type.
        /// </summary>
        /// <typeparam name="TElement">The type of object returned by the query.</typeparam>
        /// <param name="sql">The SQL query string.</param>
        /// <param name="parameters">The parameters to apply to the SQL query string.</param>
        /// <returns>Result</returns>
        public IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            return this.Database.SqlQuery<TElement>(sql, parameters);
        }

        public static int[] Batch(List<string> sqlList, int batchSize)
        {
            var result = new int[sqlList.Count];
            var index = 0;
            var counter = 0;
            while (index < batchSize)
            {
                var tempList = sqlList.Skip(index).Take(batchSize);
                index = index + batchSize;
                using (var ctxTransaction = Dao.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var item in tempList)
                        {
                            result[counter++] = Dao.Database.ExecuteSqlCommand(item);
                        }
                        Dao.SaveChanges();
                        ctxTransaction.Commit();
                    }
                    catch (Exception)
                    {
                        ctxTransaction.Rollback();
                    }
                }
            }
            return result;
        }

        public static void BatchUpdate<TEntity>(List<TEntity> entityList, int batchSize) where TEntity : class
        {
            Dao.SaveChanges();
        }
    }
}
