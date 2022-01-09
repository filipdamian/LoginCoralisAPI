using LoginCoralisAPI.Entities;
using LoginCoralisAPI.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginCoralisAPI.Data
{
    public class LoginContext:IdentityDbContext<User,Role,int,IdentityUserClaim<int>,UserRole,IdentityUserLogin<int>,IdentityRoleClaim<int>,IdentityUserToken<int>>
    {
        public LoginContext(DbContextOptions<LoginContext> options) : base(options) { } //?sterge<LoginContext>
        override public DbSet<User> Users { get; set; }  //override / new
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }
        public DbSet<SessionToken> SessionTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //One to Many
            modelBuilder.Entity<Address>().HasMany(adr => adr.Orders).WithOne(ordr => ordr.Address);
            //One to One
            modelBuilder.Entity<User>().HasOne(usr => usr.Address).WithOne(adr => adr.User).OnDelete(DeleteBehavior.Cascade);
            //Many to Many
            modelBuilder.Entity<ProductOrder>().HasKey(po => new { po.productId, po.orderId });
            modelBuilder.Entity<ProductOrder>().HasOne(po => po.Product).WithMany(p => p.ProductOrders).HasForeignKey(po => po.productId);
            modelBuilder.Entity<ProductOrder>().HasOne(po => po.Order).WithMany(o => o.ProductOrders).HasForeignKey(po => po.orderId);
            //Many to Many Users - Roles
           modelBuilder.Entity<UserRole>(ur => { ur.HasKey(ur => new { ur.UserId, ur.RoleId });
              ur.HasOne(ur => ur.Role).WithMany(r => r.UserRoles).HasForeignKey(ur => ur.RoleId);
                ur.HasOne(ur => ur.User).WithMany(u => u.UserRoles).HasForeignKey(ur => ur.UserId);

            });


        }





    }
}
