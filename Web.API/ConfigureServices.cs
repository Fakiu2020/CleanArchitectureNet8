using Application.Services;
using Domain.Entities;
using Infrastructure.AutoMapper;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace Web.API
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IAssetService, AssetService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddScoped<ApplicationDbContextInitialiser>();
            services.AddHttpContextAccessor();
            services.AddHealthChecks()
                    .AddDbContextCheck<DataContext>(); 
            services.AddDefaultIdentity<User>().AddRoles<IdentityRole<int>>().AddEntityFrameworkStores<DataContext>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(options =>
           {
               options.SaveToken = true;
               options.RequireHttpsMetadata = false;
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidAudience = configuration["JWT:ValidAudience"],
                   ValidIssuer = configuration["JWT:ValidIssuer"],
                   IssuerSigningKey =
                       new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]!))
               };
           });

            // Configurar Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

                // Definir el esquema de seguridad JWT
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
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

            return services;

        }


        public static IServiceCollection AddBaseProjectDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlServerConnection");

            return services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(connectionString));

        }

        public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services)
        {
            return services.AddAutoMapper(new Assembly[] { typeof(MappingProfile).GetTypeInfo().Assembly });
        }


        public static IServiceCollection AddBaseProjectIdentity(this IServiceCollection services)
        {
            return services;
            //services.AddIdentity<User, Role>(options =>
            //{
            //    options.Password.RequiredLength = 8;
            //    options.Password.RequireLowercase = false;
            //    options.Password.RequireUppercase = false;
            //    options.Password.RequiredUniqueChars = 0;
            //    options.Password.RequireDigit = false;
            //    options.Password.RequireNonAlphanumeric = false;
            //})
            //    .AddUserStore<UserStore>()
            //    .AddRoleStore<RoleStore>()
            //    .AddUserManager<UserManager>()
            //    .AddRoleManager<RoleManager>()
            //    .AddEntityFrameworkStores<DataContext>()
            //    .AddDefaultTokenProviders();
            //return services;
        }
    }
}
