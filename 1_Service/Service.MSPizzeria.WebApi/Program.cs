#region REFERENCES
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Domain.MSPizzeria.Entity.Models.v1;
using Infrastructure.MSPizzeria.Data;
using Service.MSPizzeria.WebApi.Modules.Authentication;
using Service.MSPizzeria.WebApi.Modules.Feature;
using Service.MSPizzeria.WebApi.Modules.Injection;
using Service.MSPizzeria.WebApi.Modules.Mapper;
using Service.MSPizzeria.WebApi.Modules.MediatR;
using Service.MSPizzeria.WebApi.Modules.Validator;

#endregion

#region PROPIEDADES POR DEFECTO DE LA CLASE PROGRAM
var builder = WebApplication.CreateBuilder(args);
#endregion

#region AUTENTICACION
builder.Services.addAuthentication(builder.Configuration);
#endregion

#region CODIGO POR DEFECTO PARA AGREGAR LOS CONTROLADORES
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
#endregion

#region MIS MODULOS

#region CONFIGURACIN DE SWAGGER CON TOKEN
builder.Services.AddSwaggerE();
#endregion

#region AGREGAR CORS
builder.Services.AddFeature(builder.Configuration);
#endregion

#region HASH / Security
builder.Services.addHashSecurity();
#endregion

#region REGISTRO DE MAPPER
builder.Services.AddMapper();
#endregion

#region REFISTRO DE MEDIATR
builder.Services.AddMediatr();
#endregion

#region INYECTAR MIS DEPENDENCIAS
/*
 * CARGAR ARCHIVO DE CONFIGURACIONES
 * INYECCION DEL SERVICIO ConnectionFactory PARA LA CONEXION A BASE DE DATOS
 * INYECCION DE LOS SERVICIOS EN LA CAPA DE APLICACION, DOMINIO e INFRAESTRUCTURA
 */
builder.Services.addInjection(builder.Configuration);
#endregion

#region AREGREGAR VALIDADOR PARA LOS PARAMETROS DE ENTRADA
builder.Services.AddValidator();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
#endregion

#region CONFIGURACION DE ENTITY FRAMEWORK REQUERIDO PARA LAS MIGRACIONES\

//var sas = builder.Configuration.GetConnectionString("defaultConnection");
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(connectionString: builder.Configuration.GetConnectionString("defaultConnection"),
        sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
        });
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDBContext>()
    .AddDefaultTokenProviders();
#endregion

#endregion

#region APP MIDDLEWARE
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

#region CORS
app.UseCors();
#endregion

#region Authentication
app.UseAuthentication();
#endregion

app.UseAuthorization();

app.MapControllers();

app.Run();
#endregion
