using ChartAccountDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChartAccountRepository
{
    public interface IGenericRepository<T> where T : class
    {

        IEnumerable<T> Get(Expression<Func<T, bool>> filter = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           string includeProperties = null);
        T GetById(object id) ;
        void Insert(T entity, bool autoSave = true);
        void Update(T entity, bool autoSave = true);
        void Delete(object id, bool autoSave = true);
        void SaveChanges();
        IQueryable<T> GetAll();
    }
}
