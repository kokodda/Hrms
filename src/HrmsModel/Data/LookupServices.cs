using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using HrmsModel.Models;

namespace HrmsModel.Data
{
    public interface ILookupServices
    {
        IEnumerable<ILookup> GetLookupItems<T>() where T : class, ILookup;
        void AddLookupItem<T>(LookupItem item) where T : class, ILookup, new();
        void UpdateLookupItem<T>(LookupItem item) where T : class, ILookup;
        void RemoveLookupItem<T>(int id) where T : class, ILookup;
        void Refresh<T>() where T : class, ILookup;
    }

    public class LookupServices : ILookupServices
    {
        private readonly HrmsDbContext _context;
        private readonly IMemoryCache _memCache;
        public LookupServices(HrmsDbContext context, IMemoryCache memCache)
        {
            _context = context;
            _memCache = memCache;
        }

        public IEnumerable<ILookup> GetLookupItems<T>() where T : class, ILookup
        {
            string cacheKey = "memoryCacheKey-" + typeof(T).Name.ToUpper();
            List<T> lst;

            if (!_memCache.TryGetValue(cacheKey, out lst))
            {
                lst = _context.Set<T>().AsNoTracking().Where(b => b.IsActive).OrderBy(b => b.SortOrder).ToList() as List<T>;
                _memCache.Set(cacheKey, lst, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(365)));
            }
            else
            {
                lst = _memCache.Get(cacheKey) as List<T>;
            }
            return lst;
        }

        //add lookup item
        public void AddLookupItem<T>(LookupItem item) where T : class, ILookup, new()
        {
            T modelItem = new T();
            modelItem.Name = item.Name;
            modelItem.SysCode = item.SysCode.ToUpper();
            modelItem.SortOrder = item.SortOrder;
            modelItem.IsActive = true;
            _context.Set<T>().Add(modelItem);
            _context.SaveChanges();
            Refresh<T>();
        }

        //update lookup item
        public void UpdateLookupItem<T>(LookupItem item) where T : class, ILookup
        {
            var modelItem = _context.Set<T>().SingleOrDefault(b => b.Id == item.Id);
            modelItem.Name = item.Name;
            modelItem.SortOrder = item.SortOrder;
            modelItem.IsActive = item.IsActive;
            _context.SaveChanges();
            Refresh<T>();
        }

        //deactivate lookup item
        public void RemoveLookupItem<T>(int id) where T : class, ILookup
        {
            var modelItem = _context.Set<T>().SingleOrDefault(b => b.Id == id);
            _context.Set<T>().Remove(modelItem);
            _context.SaveChanges();
            Refresh<T>();
        }

        //refreshes cache after insert, update or delete in the lookup
        public void Refresh<T>() where T : class, ILookup
        {
            string cacheKey = "memoryCacheKey-" + typeof(T).Name.ToUpper();
            List<ILookup> lst;

            lst = _context.Set<T>().AsNoTracking().ToList() as List<ILookup>;
            _memCache.Set(cacheKey, lst, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(365)));
        }
    }
}
