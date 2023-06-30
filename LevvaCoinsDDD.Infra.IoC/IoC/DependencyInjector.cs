using LevvaCoinsDDD.Application;
using LevvaCoinsDDD.Application.Interfaces.Services;
using LevvaCoinsDDD.Application.Mapper;
using LevvaCoinsDDD.Application.Services;
using LevvaCoinsDDD.Infra.Data.Context;
using LevvaCoinsDDD.Infra.Data.Interfaces.Repositories;
using LevvaCoinsDDD.Infra.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LevvaCoinsDDD.Infra.IoC.IoC;
public static class DependencyInjector
{
    public static void AddLevvaService(this IServiceCollection service, IConfiguration config)
    {
        service.AddMvc(config =>
        {
            var policy = new AuthorizationPolicyBuilder()
                            .RequireAuthenticatedUser()
                            .Build();
            config.Filters.Add(new AuthorizeFilter(policy));
        });

        service.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }
        ).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(config.GetSection("Secret").Value)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        service.AddDbContext<AppDBContext>(options => options.UseMySQL(config.GetConnectionString("DefaultConnection")));
        service.AddAutoMapper(typeof(DefaultMapper));

        service.AddScoped<IUserRepository, UserRepository>();
        service.AddScoped<ITransactionRepository, TransactionRepository>();
        service.AddScoped<ICategoryRepository, CategoryRepository>();

        service.AddScoped<IUserService, UserService>();
        service.AddScoped<ITransactionService, TransactionService>();
        service.AddScoped<ICategoryService, CategoryService>();

        service.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(MediatREntryPoint).Assembly));

    }
}
