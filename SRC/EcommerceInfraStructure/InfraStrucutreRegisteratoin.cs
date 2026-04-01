using Ecommerce.Core.Intefaces;
using Ecommerce.Core.Models;
using Ecommerce.Core.Services;
using EcommerceInfraStructure.Data;
using EcommerceInfraStructure.Repositries;
using EcommerceInfraStructure.Repositries.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceInfraStructure
{
    public static class InfraStrucutreRegisteratoin
    {
        public static IServiceCollection infrastructureConfiguration(this IServiceCollection services,IConfiguration configuration)
        {
            //serices.AddTransient
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepositories<>));
            //services.AddScoped<ICategoryRepository, CategoryRepository>();
            //services.AddScoped<IProductRepository, ProductRepository>();
            //services.AddScoped<IPhotoRepository, PhotoRepository>();
            services.AddScoped<IUniOfwork, UnitOFwork>();
            // register Email sender
            services.AddScoped<IEmailService, EmailService>();
            //register order services
            services.AddScoped<IOrderService, OrderService>();
            //register payment service
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IRating, RatingRepositry>();
            //apply Redis Connection
            services.AddSingleton<IConnectionMultiplexer>(i =>
            {
                var config = ConfigurationOptions.Parse(configuration.GetConnectionString("redis"));
                return ConnectionMultiplexer.Connect(config);
            });

            services.AddScoped<IGenerateToken, GenerateToken>();

            services.AddScoped<IImageManagementService, ImageManagementSerice>();
            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
            services.AddDbContext<AppDbContext>(options =>{
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();


            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
               .AddCookie(x =>
               {
                   x.Cookie.Name = "token";
                   x.Events.OnRedirectToLogin = context =>
                   {
                       context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                       return Task.CompletedTask;
                   };
               })
               .AddJwtBearer(x =>
               {
                   x.RequireHttpsMetadata = false;
                   x.SaveToken = true;
                   x.TokenValidationParameters = new TokenValidationParameters
                   {
               ValidateIssuerSigningKey = true,

               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Secret"])),
               ValidateIssuer = true,
               ValidIssuer = configuration["Token:Issuer"],
               ValidateAudience = false,
               ClockSkew = TimeSpan.Zero
           };
           x.Events = new JwtBearerEvents
           {

               OnMessageReceived = context =>
               {
                   var token = context.Request.Cookies["token"];
                   context.Token = token;
                   return Task.CompletedTask;
               }
           };
       });



            return services;
        }
    }
}
