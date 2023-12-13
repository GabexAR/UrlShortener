using Microsoft.EntityFrameworkCore;
using UrlShortener.Helpers;
using UrlShortener.Data.Entities;

namespace UrlShortener.Data
{
    public class UrlShortenerContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Url> Urls { get; set; }
        public DbSet<Category> Categories { get; set; }
        public UrlShortenerContext(DbContextOptions<UrlShortenerContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            User admin = new User()
            {
                Id = 1,
                Username = "Admin",
                Email = "admin@mail.com",
                PasswordHash = Hashing.GetPasswordHash("admin"),
                Role = Models.Enum.RoleEnum.Admin
            };

            Url url = new Url()
            {
                Id = 1,
                LargeUrl = "https://google.com",
                ShortUrl = Shortener.GetShortUrl(),
                Count = 0,
                CategoryId = 1,
                UserId = 1
            };
            Category category = new Category()
            {
                Id = 1,
                NameCategory = "Ciencia",

            };
            modelBuilder.Entity<User>(user =>
            {
                user.HasData(admin);
                user.HasMany(us => us.Urls);

            });
            modelBuilder.Entity<Category>(c =>
            {
                c.HasData(category);
                c.HasMany(u => u.Urls);

            });

            modelBuilder.Entity<Url>(u =>
            {
                u.HasData(url);
            });

            modelBuilder.Entity<User>()
                .HasMany<Url>(u => u.Urls)
                .WithOne(c => c.User);


            modelBuilder.Entity<Category>()
                .HasMany<Url>(u => u.Urls)
                .WithOne(c => c.Category);

            //modelBuilder.Entity<Url>()
            //    .HasOne(x => x.Category)
            //    .WithMany(x => x.Urls)
            //    .HasForeignKey(x => x.CategoryId)
            //    .OnDelete(DeleteBehavior.ClientSetNull);

            //modelBuilder.Entity<Url>()
            //    .HasOne(x => x.User)
            //    .WithMany(x => x.Urls)
            //    .HasForeignKey(x => x.UserId)
            //    .OnDelete(DeleteBehavior.ClientSetNull);


            base.OnModelCreating(modelBuilder);
        }
    }
}

            //modelBuilder.Entity<Url>()
            //  .HasMany(url => url.UrlCategories) // Corregido: "UrlCategories" en lugar de "Categories"
            //  .WithMany(category => category.Urls) // Corregido: "Urls" en lugar de "Url"
            //  .UsingEntity<Dictionary<string, object>>(
            //      "UrlCategorys",
            //      i => i.HasOne<Category>().WithMany().HasForeignKey("CategoryId"),
            //      i => i.HasOne<Url>().WithMany().HasForeignKey("UrlId")
            //  );

            //modelBuilder.Entity<Url>()
            // .HasMany(url => url.UrlCategories)
            // .WithOne(urlCategory => urlCategory.Url)
            // .HasForeignKey(urlCategory => urlCategory.UrlId);
            // En tu DbContext