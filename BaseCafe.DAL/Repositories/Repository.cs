using BaseCafe.DAL.Context;
using BaseCafe.DAL.Entities.Abstract;
using BaseCafe.DAL.Entities.Enum;
using BaseCafe.UI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCafe.DAL.Repositories
{
    public  class Repository<T> : IRepository<T> where T : BaseEntity, new()
    {
        private readonly MyDbContext _context;

        public Repository(MyDbContext dbContext)
        {
            _context = dbContext;
        }

        /// <summary>
        /// Veritabanında ilgili entity türüne karşılık gelen Dbseti döner
        /// </summary>
        /// 

        public DbSet<T> Table { get => _context.Set<T>(); }


        /// <summary>
        /// yeni varlık ekler ve eklenen entity döner
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>

        public T Add(T entity)
        {
            entity.Status = DataStatus.Created;
            entity.CreatedDate = DateTime.Now;
            var addedEntity = Table.Add(entity);
            Save();
            var newEntity = addedEntity.Entity;
            return newEntity;
        }

        /// <summary>
        /// bir liste halinde nesneyi ekler ve listeyi döner
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>

        public List<T> AddRange(List<T> entities)
        {
            foreach (var entity in entities)
            {
                Add(entity);
            }
            return entities;
        }

        /// <summary>
        /// velirtilen nesneyi siler ve silinen nesneyi döner
        /// </summary>
        /// <param name="entity"></param>silinmek istenen nesne
        /// <returns></returns>

        public T Delete(T entity)
        {
            entity.Status = DataStatus.Deleted;
            entity.DeletedDate = DateTime.Now;
            Table.Update(entity);
            Save();
            return entity;
        }

        /// <summary>
        /// belirtilen IDye göre entity nesnesini bulur
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public T Find(int id)
        {
            return Table.Find(id);
        }

        /// <summary>
        /// belirtilen koşula göre tek bir entity nesnesi getirir.
        /// </summary>
        /// <param name="predicate">entity için filtre ifadesi(sorgu)</param>
        /// <returns></returns>

        public T Get(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> quetable = Table;
            return quetable.FirstOrDefault(predicate);
        }

        /// <summary>
        /// tüm entity nesnelerinin listesini döner
        /// </summary>
        /// <returns></returns>


        public IList<T> GetAll()
        {
            IQueryable<T> queryable = Table;
            return queryable.ToList();
        }

        /// <summary>
        /// belirtilen entity nesnesinin veri takibinden kaldırır ve kaldırılan nesneyi döner
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T Remove(T entity)
        {
            var trackedEntity = Table.Local.FirstOrDefault(e => e.Id == entity.Id);
            if(trackedEntity != null)
            {
                Table.Remove(trackedEntity);
            }
            else
            {
                Table.Attach(entity);
                Table.Remove(entity);
            }
            Save();
            return entity;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        /// <summary>
        /// belirtilen entity nesnesini günceller ve güncellenen nesneyi döner
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>

        public T Update(T entity)
        {
            if (entity.Status != DataStatus.Deleted)
            {
                entity.Status=DataStatus.Modifed;
                entity.DeletedDate = DateTime.Now;
            }
            Table.Update(entity);
            Save();
            return entity;
        }
    }
}
