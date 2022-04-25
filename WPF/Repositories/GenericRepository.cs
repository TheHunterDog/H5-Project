using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WPF.Interfaces;
using WPF.Model;

namespace WPF.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly StudentBeleidContext _context;

    /// <summary>
    /// Repository constructor.
    /// </summary>
    /// <param name="context"><see cref="StudentBeleidContext"/></param>
    public GenericRepository(StudentBeleidContext context)
    {
        _context = context;
    }
    
    /// <summary>
    /// Add an entity to the context
    /// </summary>
    /// <param name="entity"><see cref="T"/></param>
    public void Add(T entity)
    {
        _context.Set<T>().Add(entity);
    }
    
    /// <summary>
    /// Add multiple entity's
    /// </summary>
    /// <param name="entities"><see cref="IEnumerable{T}"/></param>
    public void AddRange(IEnumerable<T> entities)
    {
        _context.Set<T>().AddRange(entities);
    }
    
    /// <summary>
    /// Match expression to Entity
    /// </summary>
    /// <param name="expression">Check with</param>
    /// <returns><see cref="IEnumerable{T}"/></returns>
    public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().Where(expression);
    }
    
    /// <summary>
    /// Get all of one entity
    /// </summary>
    /// <returns><see cref="IEnumerable{T}"/></returns>
    public IEnumerable<T> GetAll()
    {
        return _context.Set<T>().ToList();
    }
    
    /// <summary>
    /// Get one entity with id
    /// </summary>
    /// <param name="id">The id that is matched</param>
    /// <returns><see cref="T"/></returns>
    public T? GetById(int id)
    {
        return _context.Set<T>().Find(id);
    }
    
    /// <summary>
    /// Remove one entity from context
    /// </summary>
    /// <param name="entity"><see cref="T"/></param>
    public void Remove(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    /// <summary>
    /// Remove one entity with ID.
    /// </summary>
    /// <param name="id">Id number that is matched</param>
    /// <exception cref="KeyNotFoundException">If no entity is found Exception is thrown</exception>
    public void Remove(int id)
    {
        _context.Set<T>().Remove(this.GetById(id) ?? throw new KeyNotFoundException());
    }

    /// <summary>
    /// Set EntityState to modified
    /// </summary>
    /// <param name="entity"><see cref="T"/></param>
    public void Update(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
    }

    /// <summary>
    /// Remove multiple entities from context
    /// </summary>
    /// <param name="entities"></param>
    public void RemoveRange(IEnumerable<T> entities)
    {
        _context.Set<T>().RemoveRange(entities);
    }

    /// <summary>
    /// Save the context to the database
    /// </summary>
    public void Save()
    {
        _context.SaveChanges();
    }
    
}