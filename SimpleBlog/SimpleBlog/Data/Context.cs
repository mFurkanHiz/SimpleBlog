using Microsoft.EntityFrameworkCore;
using SimpleBlog.Entities;
using System.Collections.Generic;
using System.Diagnostics;

namespace SimpleBlog.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
        : base(options)
        {
        }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
