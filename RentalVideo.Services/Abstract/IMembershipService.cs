﻿using RentalVideo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalVideo.Services.Abstract
{
    public interface IMembershipService
    {
        MembershipContext ValidateUser(string username, string password);
        User CreateUser(string username, string email, string password, int[] roles);
        User GetUser(int userId);
        List<Role> GetUserRole(string username);
    }
}
