using System.Linq.Expressions;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess.EntityFramework;

public class EfEntityRepositoryBase<TEntity, TContext>
    : IEntityRepository<TEntity>
    where TEntity : class, IEntity, new()
    where TContext : DbContext,new()
{
    /// <summary>
    /// Tek bir adet entity doner. Donebilen entity 
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public TEntity Get(Expression<Func<TEntity, bool>> filter)
    {
        // short using usage
        using var context = new TContext();

        // maybe return null
        return context.Set<TEntity>().SingleOrDefault(filter);
    }


    public virtual IList<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
    {
        // short using usage
        using var context = new TContext();
        return filter is null
            ? context.Set<TEntity>().ToList()
            : context.Set<TEntity>().Where(filter).ToList();
    }

    public void Add(TEntity entity)
    {
        // disposible pattern
        using (var context = new TContext())
        {
            //Gonderilen entity'yi context'e abone ediyoruz. 
            var addedEntityEntry = context.Entry(entity);
            addedEntityEntry.State = EntityState.Added;
            context.SaveChanges();
        }
    }

    public void Update(TEntity entity)
    {
        // disposible pattern
        using (var context = new TContext())
        {
            //Gonderilen entity'yi context'e abone ediyoruz. 
            var updatEntityEntry = context.Entry(entity);
            updatEntityEntry.State = EntityState.Modified;
             context.SaveChanges();
        }
    }

    public void Delete(TEntity entity)
    {
        // disposible pattern
        using (var context = new TContext())
        {
            //Gonderilen entity'yi context'e abone ediyoruz. 
            var deletEntityEntry = context.Entry(entity);
            deletEntityEntry.State = EntityState.Deleted;
            context.SaveChanges();
        }

       
    }
}