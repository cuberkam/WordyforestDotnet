using WordyforestDotnet.BusinessLayer.Services.Abstract;
using WordyforestDotnet.BusinessLayer.Services.Concrete;
using WordyforestDotnet.DataAccessLayer.Repositories.Abstract;
using WordyforestDotnet.DataAccessLayer.Repositories.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WordyforestDotnet.DataAccessLayer.Context;
using EntityLayer.Entities;

namespace WordyforestDotnet.Api.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // DbContext
            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString));
            services.AddDatabaseDeveloperPageExceptionFilter();

            // Identity
            services.AddIdentity<ExtendedUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // JWT Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
                };
            });

            // DataAccessLayer
            services.AddScoped<IVocabularyRepository, VocabularyRepository>();
            services.AddScoped<IVocabulariesListRepository, VocabulariesListRepository>();
            services.AddScoped<ISubscribedListRepository, SubscribedListRepository>();

            // BusinessLayer
            services.AddScoped<IVocabularyService, VocabularyManager>();
            services.AddScoped<IVocabulariesListService, VocabulariesListManager>();
            services.AddScoped<ISubscribedListService, SubscribedListManager>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtService, JwtService>();

            return services;
        }
    }
}