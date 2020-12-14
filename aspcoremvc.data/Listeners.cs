using System;
using System.Linq;
using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Data
{
    public static class Listeners
    {
        public static void PreInsertListener(ChangeTracker changeTracker)
        {
            var datenow = DateTime.Now;
            foreach (var entity in changeTracker.Entries<EntityBase>().Where(x => x.State == EntityState.Added).ToList())
            { 
                entity.Entity.AddedById = HttpHelper.GetActiveUserId();
                entity.Entity.UpdatedById = HttpHelper.GetActiveUserId();
                entity.Entity.AddedDate = datenow;
                entity.Entity.UpdatedDate = datenow;
            }
        }

        /// <summary>
        /// Bir entity update edilirken bazı property'leri burada set edilir
        /// </summary>
        public static void UpdateListener(ChangeTracker changeTracker)
        {
            foreach (var entity in changeTracker.Entries<EntityBase>().Where(x => x.State == EntityState.Modified).ToList())
            {
                entity.Entity.UpdatedById = HttpHelper.GetActiveUserId();
                entity.Entity.UpdatedDate = DateTime.Now;
            }
        }

        /// <summary>
        /// Bir entity silinirken bazı property'leri burada set edilir
        /// </summary>
        /// 
        public static void DeleteListener(ChangeTracker changeTracker)
        {
            foreach (var entity in changeTracker.Entries<EntityBase>().Where(x => x.State == EntityState.Deleted).ToList())
            {
                entity.Entity.UpdatedById = HttpHelper.GetActiveUserId();
                entity.Entity.IsDeleted = true;
                entity.Entity.UpdatedDate = DateTime.Now;
                entity.State = EntityState.Modified;
            }
        }
    }
}