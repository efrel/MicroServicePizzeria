using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using Domain.MSPizzeria.Entity.Models.v1;
using Infrastructure.MSPizzeria.Data.Configuration;

namespace Infrastructure.MSPizzeria.Data;

public class ApplicationDBContext: IdentityDbContext<ApplicationUser>
{
    #region CONSTRUCTOR 
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):base(options)
    {

    }
    #endregion
    
    #region MAPEO DE TABLAS
    public DbSet<Product> Products { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    #endregion

    protected override void OnModelCreating(ModelBuilder builder)
    {
        #region AQUI COLOCAR PARAMETROS PRE-CARGADOS PARA QUE LA BD ARRANQUE CON LA INFO NECESARIA

        #region CONFIGURAR EL ROL ADMINISTRADOR CON LA MIGRACION DE DATOS
        var rolesAdmin = new IdentityRole()
        {
            //Generaddor de GUID / UUID : https://www.guidgenerator.com/online-guid-generator.aspx
            Id = "55157ea9-f5a2-4a25-bee0-d131e2866817",
            Name = "admin",
            NormalizedName = "admin"
        };
        builder.Entity<IdentityRole>().HasData(rolesAdmin);
        #endregion

        #region CONFIGURAR ROL USUARIO
        var rolesUserApp = new IdentityRole()
        {
            //Generaddor de GUID / UUID : https://www.guidgenerator.com/online-guid-generator.aspx
            Id = "eb27d436-e889-41a0-888c-62f419766b7d",
            Name = "user_App",
            NormalizedName = "user_App"
        };
        builder.Entity<IdentityRole>().HasData(rolesUserApp);
        #endregion

        #region CONFIGURAR ROL SUPERVISOR
        var rolesUserDashboardApp = new IdentityRole()
        {
            //Generaddor de GUID / UUID : https://www.guidgenerator.com/online-guid-generator.aspx
            Id = "d1801aab-f774-4c25-a1e3-4583ef0efb42",
            Name = "user_Dashboard_App",
            NormalizedName = "user_Dashboard_App"
        };
        builder.Entity<IdentityRole>().HasData(rolesUserDashboardApp);
        #endregion

        #endregion

        base.OnModelCreating(builder);

        #region CONFIGURACION DE TABLAS

        // Product Configuration
        builder.Entity<Product>(entity =>
        {
            entity.ToTable("Products");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
        });

        // Customer Configuration
        builder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customers");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.RegisterDate).HasDefaultValueSql("GETDATE()");
        });

        // Address Configuration
        builder.Entity<Address>(entity =>
        {
            entity.ToTable("Addresses");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Street).IsRequired().HasMaxLength(100);
            entity.Property(e => e.ExteriorNumber).IsRequired().HasMaxLength(10);
            entity.Property(e => e.InteriorNumber).HasMaxLength(10);
            entity.Property(e => e.ZipCode).IsRequired().HasMaxLength(10);
            
            entity.HasOne(d => d.Customer)
                  .WithMany(c => c.Addresses)
                  .HasForeignKey(d => d.CustomerId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // Order Configuration
        builder.Entity<Order>(entity =>
        {
            entity.ToTable("Orders");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.OrderDate).HasDefaultValueSql("GETDATE()");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18,2)");
            entity.Property(e => e.OrderStatus).IsRequired().HasMaxLength(20);
            entity.Property(e => e.PaymentMethod).IsRequired().HasMaxLength(20);

            entity.HasOne(o => o.Customer)
                  .WithMany(c => c.Orders)
                  .HasForeignKey(o => o.CustomerId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(o => o.Address)
                  .WithMany(a => a.Orders)
                  .HasForeignKey(o => o.AddressId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // OrderDetail Configuration
        builder.Entity<OrderDetail>(entity =>
        {
            entity.ToTable("OrderDetails");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18,2)");
            entity.Property(e => e.Subtotal).HasColumnType("decimal(18,2)");

            entity.HasOne(d => d.Order)
                  .WithMany(o => o.Details)
                  .HasForeignKey(d => d.OrderId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Product)
                  .WithMany(p => p.OrderDetails)
                  .HasForeignKey(d => d.ProductId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        #endregion
        
        // Agregar datos de ejemplo
        SeedData.ConfigureSeedData(builder);

    }

    

}