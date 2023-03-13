using Microsoft.EntityFrameworkCore;
using MyBlog.Models;

namespace MyBlog.Data
{
    public class MysqlContext : DbContext
    {
        public MysqlContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
    }
}