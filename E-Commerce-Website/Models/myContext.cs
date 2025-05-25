using E_Commerce_Website.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class myContext : IdentityDbContext
{
    public myContext(DbContextOptions<myContext> options): base(options) 
    {

    }
    public DbSet<Admin> tbl_Admin { get; set; }
    public DbSet<Customer> tbl_Customer { get; set; }
    public DbSet<Category> tbl_Category { get; set; }
    public DbSet<Product> tbl_Product { get; set; }
    public DbSet<Cart> tbl_Cart { get; set; }
    public DbSet<Faqs> tbl_Faqs { get; set; }
    public DbSet<Feedback> tbl_Feedbacks { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
       builder.Entity<Product>()
            .HasOne(p => p.category)
            .WithMany(c => c.Product)
            .HasForeignKey(p => p.Cat_Id);
        base.OnModelCreating(builder);
    }

}
