using RentalVideo.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentalVideo.Entities;
using RentalVideo.Data.Infrastructure;
using RentalVideo.Data.Extensions;
using System.Security.Principal;
using RentalVideo.Base;

namespace RentalVideo.Services
{
    public class MembershipService : IMembershipService
    {
        private readonly IEntityBaseRepository<User> userRepository;
        private readonly IEntityBaseRepository<Role> roleRepository;
        private readonly IEntityBaseRepository<UserRole> userRoleRepository;
        private readonly IEncryptionService encryptionService;
        private readonly IUnitOfWork unitOfWork;

        public MembershipService(IEntityBaseRepository<User> userRepository, IEntityBaseRepository<Role> roleRepository,
        IEntityBaseRepository<UserRole> userRoleRepository, IEncryptionService encryptionService, IUnitOfWork unitOfWork)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
            this.userRoleRepository = userRoleRepository;
            this.encryptionService = encryptionService;
            this.unitOfWork = unitOfWork;
        }

        private void AddUserToRole(User user, int roleId)
        {
            var role = this.roleRepository.GetSingle(roleId);
            if (role == null)
            {
                throw new ApplicationException("Role does not exist.");
            }
            var userRole = new UserRole()
            {
                RoleId = role.Id,
                UserId = user.Id
            };
            this.userRoleRepository.Add(userRole);
        }

        private bool IsPasswordValid(User user, string password)
        {
            return string.Equals(this.encryptionService.EncryptPassword(password, user.Salt), user.HashedPassword);
        }

        private bool IsUserValid(User user, string password)
        {
            if (IsPasswordValid(user, password))
            {
                return !user.IsLocked;
            }
            return false;
        }

        public MembershipContext ValidateUser(string username, string password)
        {
            var memberShipContext = new MembershipContext();
            var user = this.userRepository.GetSingleByUsername(username);
            if (user != null && IsUserValid(user, password))
            {
                var userRoles = GetUserRole(user.Username);
                memberShipContext.User = user;
                var identity = new GenericIdentity(user.Username);
                memberShipContext.Principal = new GenericPrincipal(identity,
                    userRoles.Select(x => x.Name).ToArray());
            }
            return memberShipContext;
        }

        public User CreateUser(string username, string email, string password, int[] roles)
        {
            var existingUser = this.userRepository.GetSingleByUsername(username);
            if (existingUser != null)
            {
                throw new Exception("Username is already in use.");
            }
            var salt = this.encryptionService.CreateSalt();
            var user = new User()
            {
                Username = username,
                Salt = salt,
                Email = email,
                IsLocked = false,
                HashedPassword = this.encryptionService.EncryptPassword(password, salt),
                DateCreated = SystemInformation.GetDate()
            };
            this.userRepository.Add(user);
            this.unitOfWork.Commit();
            if (roles  != null || roles.Length > 0)
            {
                foreach (var role in roles)
                {
                    AddUserToRole(user, role);
                }
            }
            this.unitOfWork.Commit();
            return user;
        }

        public User GetUser(int userId)
        {
            return this.userRepository.GetSingle(userId);
        }

        public List<Role> GetUserRole(string username)
        {
            var result = new List<Role>();
            var existingUser = this.userRepository.GetSingleByUsername(username);
            if (existingUser != null)
            {
                foreach (var userRole in existingUser.UserRoles)
                {
                    result.Add(userRole.Role);
                }
            }
            return result.Distinct().ToList();
        }
    }
}
