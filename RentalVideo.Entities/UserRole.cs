namespace RentalVideo.Entities
{
    // A user may have more than one roles so we have a UserRole Entity
    public class UserRole : IEntityBase
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}