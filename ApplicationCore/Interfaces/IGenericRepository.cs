using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        // Get object by it's key id
        T GetById(int id);

        T GetByClientId(string id);
        // predicate is used to verify a condition on an object 
        // noTracking is ReadOnly Results, and Includes is Join of other objects 
        T Get(Expression<Func<T, bool>> predicate, bool asNoTracking = false, string includes = null);

        //Returns an enumerable list results  (something that can be looped/stepped through one at a time)
        IEnumerable<T> List();
        IEnumerable<T> List(Expression<Func<T, bool>> predicate, Expression<Func<T, int>> orderBy = null, string includes = null);
        Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, int>> orderBy = null, string includes = null);

        Task<T> GetAsync(Expression<Func<T, bool>> predicate, bool asNoTracking = false, string includes = null);


        //Add (insert) single entity
        void Add(T entity);

        //Delete single entity
        void Delete(T entity);

        //Delete a bunch of entities
        void Delete(IEnumerable<T> entities);

        //Update a single entity
        void Update(T entity);
    }
}
