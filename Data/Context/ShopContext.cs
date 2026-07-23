using Core.Concretes.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class ShopContext : IdentityDbContext<Member>
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {
        }

        // Kullanıcı ve Profil Modelleri
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Store> Stores { get; set; }

        // E-Ticaret Modelleri
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }
        public virtual DbSet<ProductFeature> ProductFeatures { get; set; }

        // Sepet - Sipariş Modelleri
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<CartItem> CartItems { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region AutoInclude Yapıları
            builder.Entity<Product>()
                .Navigation(p => p.Category)
                .AutoInclude();

            builder.Entity<Product>()
                .Navigation(p => p.Brand)
                .AutoInclude();

            builder.Entity<Product>()
                .Navigation(p => p.Store)
                .AutoInclude();

            builder.Entity<Product>()
                .Navigation(p => p.Images)
                .AutoInclude();

            builder.Entity<Cart>()
                .Navigation(c => c.Items)
                .AutoInclude();

            builder.Entity<CartItem>()
                .Navigation(c => c.Product)
                .AutoInclude();
            #endregion

            #region Birebir ilişkiler (Kimlik ve Profiller)
            builder.Entity<Admin>()
                .HasOne(a => a.Member)
                .WithOne(m => m.AdminProfile)
                .HasForeignKey<Admin>(a => a.MemberId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Customer>()
                .HasOne(c => c.Member)
                .WithOne(m => m.CustomerProfile)
                .HasForeignKey<Customer>(c => c.MemberId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Store>()
                .HasOne(s => s.Member)
                .WithOne(m => m.StoreProfile)
                .HasForeignKey<Store>(s => s.MemberId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region E-Ticaret ilişkileri

            // Kategori -> AltKategori
            builder.Entity<Category>()
               .HasOne(c => c.ParentCategory)
               .WithMany(c => c.SubCategories)
               .HasForeignKey(c => c.ParentCategoryId)
               .OnDelete(DeleteBehavior.Restrict);

            // Ürün -> Marka
            builder.Entity<Product>()
                .HasOne(p => p.Brand)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.BrandId)
                .OnDelete(DeleteBehavior.Restrict);

            // Ürün -> Kategori
            builder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Ürün -> Mağaza
            builder.Entity<Product>()
                .HasOne(p => p.Store)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.StoreId)
                .OnDelete(DeleteBehavior.Restrict);

            // Ürün Görseli -> Ürün
            builder.Entity<ProductImage>()
                .HasOne(pi => pi.Product)
                .WithMany(p => p.Images)
                .HasForeignKey(pi => pi.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // Ürün Özelliği -> Ürün
            builder.Entity<ProductFeature>()
                .HasOne(pf => pf.Product)
                .WithMany(p => p.Features)
                .HasForeignKey(pf => pf.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // Sepet -> Üye
            builder.Entity<Cart>()
                .HasOne(c => c.Member)
                .WithMany(p => p.Carts)
                .HasForeignKey(c => c.MemeberId)
                .OnDelete(DeleteBehavior.Cascade);

            // Sepet Ürünü -> Sepet
            builder.Entity<CartItem>()
                .HasOne(ci => ci.Cart)
                .WithMany(c => c.Items)
                .HasForeignKey(c => c.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            // Sepet Ürünü -> Ürün
            builder.Entity<CartItem>()
                .HasOne(ci => ci.Product)
                .WithMany(p => p.CartItems)
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            // Bazı sütun ayarları da burada yapılabilir.
            builder.Entity<Store>()
                .Property(s => s.CommissionRate)
                .HasPrecision(18, 2);

            builder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);
        }
    }
}
