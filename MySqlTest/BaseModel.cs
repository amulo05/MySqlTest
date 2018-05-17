using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlTest
{
    public class BaseModel<T> where T : class
    {
        #region Fields

        private IDbSet<T> _entities;

        #endregion

        public List<T> Find(string queryString)
        {
            return Db.Dao.SqlQuery<T>(queryString).ToList<T>();
        }

        public T FindFirst(string queryString)
        {
            return Db.Dao.SqlQuery<T>(queryString).FirstOrDefault();
        }

        public T FindById(object[] idValues)
        {
            return Db.Dao.Set<T>().Find(idValues);
        }

        public void Save()
        {
            this.Entities.Add(this as T);
            Db.Dao.SaveChanges();
        }

        public void Update()
        {
            Db.Dao.SaveChanges();
        }

        /// <summary>
        /// Entities
        /// </summary>
        protected virtual IDbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = Db.Dao.Set<T>();
                return _entities;
            }
        }
    }
}
