using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using Domain.MSPizzeria.Entity.Models.v1;

namespace Service.MSPizzeria.WebApi;

public class ApplicationDBContext: IdentityDbContext<ApplicationUser>
{
    #region CONSTRUCTOR 
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):base(options)
    {

    }
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

    }

    #region MAPEO DE TABLAS
    //public DbSet<users> usuarios { get; set; }
    #endregion

}