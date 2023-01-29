using ChartAccountDomain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChartAccountRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly MainDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(MainDbContext context)
        {
            this._context = context;
            this._dbSet = context.Set<T>();
        }

        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

   
        public void Delete(object id, bool autoSave = true)
        {
            var entityToDelete = GetById(id);

            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);

            if(autoSave) SaveChanges();
        }



        public T GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public void Insert(T entity, bool autoSave = true)
        {
            _dbSet.Add(entity);
            if (autoSave) SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(T entity, bool autoSave = true)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            
            if (autoSave) SaveChanges();
        }

        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }
    }
}

