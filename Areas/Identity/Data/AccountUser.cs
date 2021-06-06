using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Bank.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the AccountUser class
    public class AccountUser : IdentityUser
    {
       public bool notif { get; set; }
    }
}
