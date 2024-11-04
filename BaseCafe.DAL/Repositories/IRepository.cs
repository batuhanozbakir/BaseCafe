using BaseCafe.DAL.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BaseCafe.UI
{
    public interface IRepository <T> where T : BaseEntity, new()
    {
        DbSet<T> Table { get; }

        public T Add(T entity);
        public T Update(T entity);
        public T Delete(T entity);
        public T Remove(T entity);
        public List<T> AddRange(List<T> entities);
        public IList<T> GetAll();
        public T Get(Expression<Func<T, bool>> predicate);
        public void Save();
        public T Find(int id);
    }
}
