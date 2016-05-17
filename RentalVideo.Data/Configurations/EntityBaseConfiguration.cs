using RentalVideo.Entities;
using System.Data.Entity.ModelConfiguration;

namespace RentalVideo.Data.Configurations
{
    public class EntityBaseConfiguration<T> : EntityTypeConfiguration<T> where T : class, IEntityBase
    {
        public EntityBaseConfiguration()
        {
            HasKey(e => e.Id);
        }
    }
}
