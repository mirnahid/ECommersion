﻿using ECommersionAPI.Domain.Entities;
using ECommersionAPI.Domain.Entities.Common;
using ECommersionAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommersionAPI.Persistence.Contexts
{
    public class ECommersionAPIDbContext : IdentityDbContext<AppUser,AppRole,string>
    {
        public ECommersionAPIDbContext(DbContextOptions<ECommersionAPIDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ECommersionAPI.Domain.Entities.File> Files { get; set; }
        public DbSet<ProductImageFile> ProductImageFiles { get; set; }
        public DbSet<InvoiceFile> InvoiceFiles { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker
                  .Entries<BaseEntity>();

            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow,
                    _=>DateTime.UtcNow
                };

            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
