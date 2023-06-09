﻿using GameForum1.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameForum1.Data;

public class GameForum1Context : IdentityDbContext<GameForum1User>
{
    public GameForum1Context(DbContextOptions<GameForum1Context> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
    public virtual DbSet<DbPrivateMessage> PrivateMessages { get; set; }
    public DbSet<AppFile> File { get; set; }
}
