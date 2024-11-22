using Microsoft.EntityFrameworkCore;
using CartService.Model;

namespace CartService.Data
{
    public class CartContext : DbContext
    {
        public CartContext(DbContextOptions<CartContext> options) : base(options) { }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships and constraints
            modelBuilder.Entity<CartItem>()
                .HasOne<Cart>()
                .WithMany(c => c.Items)
                .HasForeignKey(ci => ci.CartId);
        }
    }
}