using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Shoesify.Services.Abstractions;
using Shoesify.Services.Requests;
using Shoesify.Services.Validators;

namespace Shoesify.Services;

public static class DependencyInjection
{
    public static void AddAuthenticationServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(options =>
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
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],  
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!))  
                };
            });
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<JwtTokenService>();
        builder.Services.AddScoped<AuthenticationService>();
    }
    
    public static void AddServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<IInventoryService, InventoryService>();
    }

    public static void AddValidators(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<IValidator<CreateInventoryRequest>, CreateInventoryRequestValidator>();
        builder.Services.AddScoped<IValidator<UpdateInventoryRequest>, UpdateInventoryRequestValidator>();
        builder.Services.AddScoped<IValidator<DisableInventoryRequest>, DisableInventoryRequestValidator>();
    }
}