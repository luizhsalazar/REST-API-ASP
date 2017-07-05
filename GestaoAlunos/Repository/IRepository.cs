using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GestaoAlunos.Repository
{
    public interface IRepository<T> where T : class
    {
        T Incluir(T obj);
        T Consultar(int id);
        T Alterar(T obj);
        T Excluir(T obj);
        List<T> GetAll();
        List<T> GetAll(params string[] includes);

        void Salvar();

        IQueryable<T> BuscarComInclude<TProperty>(Expression<Func<T, bool>> predicate, params Expression<Func<T, TProperty>>[] includes);

        IQueryable<T> BuscarComInclude(Expression<Func<T, bool>> predicate, params string[] includes);
    }
}
