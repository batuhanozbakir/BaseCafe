using AutoMapper;
using BaseCafe.BLL.Managers.Abstract;
using BaseCafe.DAL.Entities.Abstract;
using BaseCafe.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BaseCafe.BLL.Managers.Concrete
{
    /// <summary>
    /// DTO ve entity üzerinde işlemlerin gerçekleştiği sınıf
    /// </summary>
    /// <param name="reposityory"></param>
    public abstract class GenericManager<TDTO, TEntity> : IGenericManager<TDTO, TEntity> where TEntity : BaseEntity, new() where TDTO : class
    {
        private readonly IMapper _mapper;
        private readonly IRepository<TEntity> _repository;
       
        protected GenericManager(IRepository<TEntity> reposityory)
        {
            var config = new MapperConfiguration(cfg =>cfg.CreateMap<TDTO, TEntity>().ReverseMap());
            _mapper = new Mapper(config);
            _repository = reposityory;
        }

        public TDTO Add(TDTO dTO)
        {
            TEntity entity = _mapper.Map<TEntity>(dTO);
            _repository.Add(entity);
            return dTO;
        }
        /// <summary>
        /// bir liste halinde DTo nesnelerini ekler ve eklenen listeyi döner
        /// </summary>
        /// <param name="dTOs"></param>
        /// <returns></returns>
        public List<TDTO> AddRange(List<TDTO> dTOs)
        {
            List<TEntity> entities = _mapper.Map<List<TEntity>>(dTOs);
            _repository.AddRange(entities);
            return dTOs;
        }

        public TDTO Delete(TDTO dTO)
        {
            TEntity entity = _mapper.Map<TEntity>(dTO);
            _repository.Delete(entity);
            return dTO;
        }

        public TDTO Find(int id)
        {
           var entity = _repository.Find(id);
           var DTO = _mapper.Map<TDTO>(entity);
           return DTO;
        }

        public TDTO Get(Expression<Func<TDTO, bool>> predicate)
        {
            var entityPredicate = _mapper.Map<Expression<Func<TEntity, bool>>>(predicate);
            var entity = _repository.Get(entityPredicate);
            var DTO = _mapper.Map<TDTO>(entity);
            return DTO;
        }
        /// <summary>
        /// Tüm DTO nesnelerinin listesini döner
        /// </summary>
        /// <param name="dTO"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IList<TDTO> GetAll()
        {
            var entities = _repository.GetAll();
            var DTOs=_mapper.Map<List<TDTO>>(entities);
            return DTOs;
        }
       /// <summary>
       /// belirtilen dto nesnesini kaldırır ve
       /// </summary>
       /// <param name="dTO"></param>
       /// <returns></returns>

        public TDTO Remove(TDTO dTO)
        {
            TEntity entity = _mapper.Map<TEntity>(dTO);
            _repository.Remove(entity);
            return dTO;
        }

        public TDTO Update(TDTO dTO)
        {
            TEntity entity = _mapper.Map<TEntity>(dTO);
            _repository.Update(entity);
            return dTO;
        }
    }
}
