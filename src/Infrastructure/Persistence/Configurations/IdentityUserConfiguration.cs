// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Persistence.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        // Each User can have many UserClaims
        // duplicate definition 
        //builder.HasMany(e => e.UserClaims)
        //    .WithOne()
        //    .HasForeignKey(uc => uc.UserId)
        //    .IsRequired();

        // Each User can have many UserLogins
        builder.HasMany(e => e.Logins)
            .WithOne()
            .HasForeignKey(ul => ul.UserId)
            .IsRequired();

        // Each User can have many UserTokens
        builder.HasMany(e => e.Tokens)
            .WithOne()
            .HasForeignKey(ut => ut.UserId)
            .IsRequired();

        // Each User can have many entries in the UserRole join table
        builder.HasMany(e => e.UserRoles)
            .WithOne()
            .HasForeignKey(ur => ur.UserId)
            .IsRequired();
    }
}


public class ApplicationRoleClaimConfiguration : IEntityTypeConfiguration<ApplicationRoleClaim>
{
    public void Configure(EntityTypeBuilder<ApplicationRoleClaim> builder)
    {
        builder.HasOne(d => d.Role)
              .WithMany(p => p.RoleClaims)
              .HasForeignKey(d => d.RoleId)
              .OnDelete(DeleteBehavior.Cascade);
    }
}
public class ApplicationUserRoleConfiguration : IEntityTypeConfiguration<ApplicationUserRole>
{
    public void Configure(EntityTypeBuilder<ApplicationUserRole> builder)
    {
        builder.HasOne(d => d.Role)
              .WithMany(p => p.UserRoles)
              .HasForeignKey(d => d.RoleId)
              .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(d => d.User)
              .WithMany(p => p.UserRoles)
              .HasForeignKey(d => d.UserId)
              .OnDelete(DeleteBehavior.Cascade);
    }
}
public class ApplicationUserClaimConfiguration : IEntityTypeConfiguration<ApplicationUserClaim>
{
    public void Configure(EntityTypeBuilder<ApplicationUserClaim> builder)
    {
        builder.HasOne(d => d.User)
              .WithMany(p => p.UserClaims)
              .HasForeignKey(d => d.UserId)
              .OnDelete(DeleteBehavior.Cascade);

    }
}
public class ApplicationUserLoginConfiguration : IEntityTypeConfiguration<ApplicationUserLogin>
{
    public void Configure(EntityTypeBuilder<ApplicationUserLogin> builder)
    {
        builder.HasOne(d => d.User)
              .WithMany(p => p.Logins)
              .HasForeignKey(d => d.UserId)
              .OnDelete(DeleteBehavior.Cascade);

    }
}
public class ApplicationUserTokenConfiguration : IEntityTypeConfiguration<ApplicationUserToken>
{
    public void Configure(EntityTypeBuilder<ApplicationUserToken> builder)
    {
        builder.HasOne(d => d.User)
              .WithMany(p => p.Tokens)
              .HasForeignKey(d => d.UserId)
              .OnDelete(DeleteBehavior.Cascade);

    }
}
