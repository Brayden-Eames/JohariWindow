using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext; //injecting database contents as 'readonly'
        }

        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges(); ;
        }

        public void Delete(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
            _dbContext.SaveChanges();
        }

        public virtual T Get(Expression<Func<T, bool>> predicate, bool asNoTracking = false, string includes = null)
        {
            if (includes == null) //if it does not include any other tables
            {
                if (asNoTracking) //check if being tracked. 
                {
                    return _dbContext.Set<T>() //return set of no objects 
                        .AsNoTracking()
                        .Where(predicate) //this is basically like WHERE clause in SQL. this is LINQ, and the predicate is the function called predicate passed into the function.
                        .FirstOrDefault(); //return a single first or default record. 
                }
                else
                {
                    return _dbContext.Set<T>()
                               .Where(predicate) //notice how 'AsNoTracking()' is not present in this conditional, which means tracking is enabled(?)
                               .FirstOrDefault();
                }
            }
            else
            {
                IQueryable<T> queryable = _dbContext.Set<T>(); //used when needing to include other tables (complex query) turns object into queryable object. 
                foreach (var includeProperty in includes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) //splits the includes string by comma, since the 'includes' holds value representing individual tables. 
                {
                    queryable = queryable.Include(includeProperty);
                }
                if (asNoTracking)
                {
                    return queryable //return set of no objects 
                        .AsNoTracking()
                        .Where(predicate) //this is basically like WHERE clause in SQL. this is LINQ, and the predicate is the function called predicate passed into the function.
                        .FirstOrDefault(); //return a single first or default record. 
                }
                else
                {
                    return queryable
                               .Where(predicate) //notice how 'AsNoTracking()' is not present in this conditional, which means tracking is enabled(?)
                               .FirstOrDefault();
                }
            }
        }

        public virtual T GetById(int id) //virtual used to modify a method, and allows to be overriden in a derrived class (child class)
        {
            return _dbContext.Set<T>().Find(id); //find particular records by a specified ID
        }

        public virtual T GetByClientId(string id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public virtual IEnumerable<T> List()
        {
            return _dbContext.Set<T>().ToList().AsEnumerable();
        }

        public virtual IEnumerable<T> List(Expression<Func<T, bool>> predicate, Expression<Func<T, int>> orderBy = null, string includes = null)
        {
            IQueryable<T> queryable = _dbContext.Set<T>();
            if (predicate != null && includes == null)
            {
                return _dbContext.Set<T>()
                           .Where(predicate)
                           .AsEnumerable();
            }
            else if (includes != null)   //assumes you have includes
            {
                foreach (var includeProperty in includes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) //splits the includes string by comma, since the 'includes' holds value representing individual tables. 
                {
                    queryable = queryable.Include(includeProperty);
                }
            }

            if (predicate == null)
            {
                if (orderBy == null)
                {
                    return queryable.AsEnumerable();
                }
                else
                {
                    return queryable.OrderBy(orderBy).ToList().AsEnumerable();
                }
            }
            else
            {
                if (orderBy == null)
                {
                    return queryable.Where(predicate).ToList().AsEnumerable();
                }
                else
                {
                    return queryable.Where(predicate).OrderBy(orderBy).ToList().AsEnumerable();
                }
            }

        }

        public virtual async Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, int>> orderBy = null, string includes = null)
        {
            IQueryable<T> queryable = _dbContext.Set<T>();
            if (predicate != null && includes == null)
            {
                return _dbContext.Set<T>()
                           .Where(predicate)
                           .AsEnumerable();
            }
            else if (includes != null)   //assumes you have includes
            {
                foreach (var includeProperty in includes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) //splits the includes string by comma, since the 'includes' holds value representing individual tables. 
                {
                    queryable = queryable.Include(includeProperty);
                }
            }

            if (predicate == null)
            {
                if (orderBy == null)
                {
                    return queryable.AsEnumerable();
                }
                else
                {
                    return await queryable.OrderBy(orderBy).ToListAsync();
                }
            }
            else
            {
                if (orderBy == null)
                {
                    return queryable.Where(predicate).ToList().AsEnumerable();
                }
                else
                {
                    return queryable.Where(predicate).OrderBy(orderBy).ToList().AsEnumerable();
                }
            }
        }

        public virtual async Task<T> GetAsync(Expression<Func<T, bool>> predicate, bool asNoTracking = false, string includes = null)
        {
            if (includes == null) //if it does not include any other tables
            {
                if (asNoTracking) //check if being tracked. 
                {
                    return await _dbContext.Set<T>() //return set of no objects 
                        .AsNoTracking()
                        .Where(predicate) //this is basically like WHERE clause in SQL. this is LINQ, and the predicate is the function called predicate passed into the function.
                        .FirstOrDefaultAsync(); //return a single first or default record. 
                }
                else
                {
                    return await _dbContext.Set<T>()
                               .Where(predicate) //notice how 'AsNoTracking()' is not present in this conditional, which means tracking is enabled(?)
                               .FirstOrDefaultAsync();
                }
            }
            else
            {
                IQueryable<T> queryable = _dbContext.Set<T>(); //used when needing to include other tables (complex query) turns object into queryable object. 
                foreach (var includeProperty in includes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) //splits the includes string by comma, since the 'includes' holds value representing individual tables. 
                {
                    queryable = queryable.Include(includeProperty);
                }
                if (asNoTracking)
                {
                    return await queryable //return set of no objects 
                        .AsNoTracking()
                        .Where(predicate) //this is basically like WHERE clause in SQL. this is LINQ, and the predicate is the function called predicate passed into the function.
                        .FirstOrDefaultAsync(); //return a single first or default record. 
                }
                else
                {
                    return await queryable
                               .Where(predicate) //notice how 'AsNoTracking()' is not present in this conditional, which means tracking is enabled(?)
                               .FirstOrDefaultAsync();
                }
            }
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified; //flag state as modified. 
            _dbContext.SaveChanges(); //save changes to the actual table in the DB
        }
    }
}
