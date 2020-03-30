// -----------------------------------------------------------------------
// <copyright file="IdentityDbContext.cs" company="Plantarium Co.">
//     Plantarium, All rights reserved
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Data.Contexts
{
    using System;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using External = Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    /// <summary>
    /// The identity database context.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext{Microsoft.AspNetCore.Identity.IdentityUser{System.Guid}, Microsoft.AspNetCore.Identity.IdentityRole{System.Guid}, System.Guid}" />
    public class IdentityDbContext : External.IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityDbContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Configures the schema needed for the identity framework.
        /// </summary>
        /// <param name="builder">The builder being used to construct the model for this context.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Identity schema
            builder.HasDefaultSchema("identity");

            // Identity tables
            builder.Entity<IdentityUser<Guid>>(entity => entity.ToTable("Users"));
            builder.Entity<IdentityRole<Guid>>(entity => entity.ToTable("Roles"));
            builder.Entity<IdentityUserRole<Guid>>(entity => entity.ToTable("UserRoles"));
            builder.Entity<IdentityUserClaim<Guid>>(entity => entity.ToTable("UserClaims"));
            builder.Entity<IdentityRoleClaim<Guid>>(entity => entity.ToTable("RoleClaims"));
            builder.Entity<IdentityUserLogin<Guid>>(entity => entity.ToTable("UserLogins"));
            builder.Entity<IdentityUserToken<Guid>>(entity => entity.ToTable("UserTokens"));

            // Identity Indexes
            builder.Entity<IdentityUser<Guid>>().HasIndex(u => u.NormalizedUserName).HasName("IX_Users_NormalizedUserName").IsUnique();
            builder.Entity<IdentityUser<Guid>>().HasIndex(u => u.NormalizedEmail).HasName("IX_Users_NormalizedEmail");
            builder.Entity<IdentityRole<Guid>>().HasIndex(r => r.NormalizedName).HasName("IX_Roles_NormalizedName").IsUnique();
        }
    }
}
