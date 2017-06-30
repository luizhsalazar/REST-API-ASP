using GestaoAlunos.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GestaoAlunos.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected DataContext DataContext;
        protected DbSet<T> DbSet;

        public Repository(DataContext dataContext)
        {
            DataContext = dataContext;
            DbSet = DataContext.Set<T>();
        }

        public T Alterar(T obj)
        {
            DbSet.Update(obj);
            //DbSet.Attach(obj);
            //DataContext.Entry(obj).State = EntityState.Modified;
            return obj;
        }

        public T Consultar(int id)
        {
            var c = DbSet.Find(id);
            return c;
        }

        public T Excluir(T obj)
        {
            DbSet.Attach(obj);
            DataContext.Entry(obj).State = EntityState.Deleted;
            return obj;
        }

        public List<T> GetAll()
        {
            return DbSet.ToList();
        }

        public T Incluir(T obj)
        {
            DbSet.Add(obj);
            return obj;
        }

        public void Salvar()
        {
            DataContext.SaveChanges();
        }

        public IQueryable<T> BuscarComInclude<TProperty>(Expression<Func<T, bool>> predicate, params Expression<Func<T, TProperty>>[] includes)
        {
            var query = DbSet.Where(predicate);

            foreach (var item in includes)
            {
                query = query.Include(item);
            }

            return query;
        }

        public IQueryable<T> BuscarComInclude(Expression<Func<T, bool>> predicate, params string[] includes)
        {
            var query = DbSet.Where(predicate);

            foreach (var item in includes)
            {
                query = query.Include(item);
            }

            return query;
        }
    }
}
