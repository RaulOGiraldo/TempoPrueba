using FluentValidation.AspNetCore;
using InsttanttFlujos.Infrastructure.Data;
using InsttanttFlujos.Infrastructure.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TempoPrueba.Core.Entities;
using TempoPrueba.Infrastructure.Extensions;
using TempoPrueba.Intrastructure.Data;

var builder = WebApplication.CreateBuilder(args);
var configutarion = builder.Configuration;

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();

// Add Cors
builder.Services.AddCors(options =>
    options.AddDefaultPolicy(builder =>
    { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().WithExposedHeaders("WWW-Authenticate"); })
);

//  Configura el JWT para la seguridad de la API
builder.Services
    .AddAuthorization()
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

    }).AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configutarion["Authentication:Issuer"],
            ValidAudience = configutarion["Authentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configutarion["Authentication:SecretKey"])),
            ClockSkew = TimeSpan.Zero
        };
    });

//   Para la configuración del Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TempoPrueba.Api", Version = "v1",
        Description = "Esto es una API de pruebas para una tabla de Proveedores en MongoDB y net core 6.0" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Name = "Authorization",
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            }, 
            new List<string>()
        }
    });
    
});

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(FilterOfExcepcion));
    options.Filters.Add<GlobalExceptionFilter>();
});

// Configurar las propiedades para el Cifrado del Password
builder.Services.Configure<PasswordOptions>(configutarion.GetSection("PasswordOptions"));

// Cadena de Conexión para Sql Server
builder.Services.AddDbContext<InsttanttFlujosContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Cadena de Conexion para Mongo
builder.Services.Configure<MongoDBProvider>(
    builder.Configuration.GetSection("MongoDbProvider")
);

// Dependencias propias - (Inyeccion de dependencias)
builder.AddServices();

// Validaciones y Filtros
builder.Services.AddMvc(options => { }
).AddFluentValidation(options =>
{
    options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
});

var app = builder.Build();

// Configure the HTTP request pipeline.  IWebHostEnvironment
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TempoPrueba.Api v1"));
    app.UseCors();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
