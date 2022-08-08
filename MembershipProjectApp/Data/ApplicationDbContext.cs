using MembershipProjectApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MembershipProjectApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ApplicationUser> AspNetUsers { get; set; }
        public virtual DbSet<ApplicationRole> AspNetRoles { get; set; }

        public DbSet<MembershipModel> Memberships { get; set; }
    }
}
