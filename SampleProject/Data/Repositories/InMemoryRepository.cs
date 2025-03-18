using BusinessEntities;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repositories
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class InMemoryRepository<T> : IInMemoryRepository<T> where T : IdObject
    {
        private static IList<T> _items = new List<T>();

        protected IQueryable<T> Items => _items.AsQueryable();
        
        public void Delete(T entity)
        {
            if(entity != null)
            {
                var itemToDelete = Get(entity.Id);
                if(itemToDelete != null)
                    _items.Remove(itemToDelete);
                    //Items.Remove(itemToDelete);
            }
        }

        public T Get(Guid id)
        {
            return Items.FirstOrDefault(i => i.Id == id);
        }

        public void Save(T entity)
        {
            if(entity != null)
            {
                var item = Get(entity.Id);
                if(item != null)
                {
                    item = entity;
                }
                else
                {
                    _items.Add(entity);
                }
            }
        }

        public IList<T> GetAll()
        {
            return _items;
            //return Items;
        }

        public void DeleteAll()
        {
            _items.Clear();
        }
    }
}
