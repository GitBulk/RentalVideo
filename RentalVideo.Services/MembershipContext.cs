using RentalVideo.Entities;
using System.Security.Principal;

namespace RentalVideo.Services
{
    public class MembershipContext
    {
        public IPrincipal Principal { get; set; }
        public User User { get; set; }
        public bool IsValid()
        {
            return this.Principal != null;
        }
    }
}