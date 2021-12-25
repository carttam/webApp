using webApp.Models;
using Microsoft.EntityFrameworkCore;

namespace webApp.Data
{
    public partial class ShopContext : DbContext
    {
        private string DbPath { get; }
        
        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        { 
            DbPath = System.IO.Path.Join(Environment.CurrentDirectory, "DBshop.db");
        }
        
        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}"); 

        public virtual DbSet<Models.Attribute>? Attributes { get; set; }
        public virtual DbSet<Brand>? Brands { get; set; }
        public virtual DbSet<Categori>? Categoris { get; set; }
        public virtual DbSet<DisCountCode>? disCountCodes { get; set; }
        public virtual DbSet<Models.Object>? Objects { get; set; }
        public virtual DbSet<Sell>? Sells { get; set; }
        public virtual DbSet<Payment>? Payments { get; set; }
        public virtual DbSet<SubCategori>? SubCategoris { get; set; }
        public virtual DbSet<Token>? Tokens { get; set; }
        public virtual DbSet<User>? Users { get; set; }

    }
}
