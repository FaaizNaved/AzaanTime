using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NamazTimeApp.Application.API.Middleware;
using NamazTimeApp.Core;
using NamazTimeApp.Data.Infrastructure;
using NamazTimeApp.Data.Infrastructure.Extensions;
using NamazTimeApp.Infrastructure.Data;
using NamazTimeApp.Presentation.Shared.Injections;
using NamazTimeApp.Presentation.Shared.MappingProfiles;
using NamazTimeApp.Realtime.Extensions;
using NamazTimeApp.Realtime.Hubs;
using Serilog;
using System.Text;

namespace NamazTimeApp.Application.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString = builder.Configuration.GetConnectionString(
            Constants.ConfigurationKeys.CONNECTION_STRING);

        builder.Services.AddDbContext<AppDbContext>(opts =>
            opts.UseNpgsql(connectionString));

        builder.Services.AddDbContext<AppMigrationDbContext>(opts =>
            opts.UseNpgsql(connectionString, options =>
            {
                options.MigrationsHistoryTable("__EFMigrationsHistory", "NAMAZTIMEAPP_MASTER");
            }));

        var jwtSecurityKey = builder.Configuration
            .GetSection(Constants.ConfigurationSections.IDENTITY_CONFIG)
            .GetSection(Constants.ConfigurationKeys.JWT_SECURITY_KEY)
            .Value ?? string.Empty;

        builder.Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration
                        .GetSection(Constants.ConfigurationSections.IDENTITY_CONFIG)
                        .GetSection(Constants.ConfigurationKeys.JWT_ISSUER)
                        .Value,
                    ValidAudience = builder.Configuration
                        .GetSection(Constants.ConfigurationSections.IDENTITY_CONFIG)
                        .GetSection(Constants.ConfigurationKeys.JWT_AUDIENCE)
                        .Value,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSecurityKey)),
                    ClockSkew = TimeSpan.Zero
                };
            });

        builder.Services.AddAuthorization();

        builder.Services.AddApplicationRepositories();
        builder.Services.AddApplicationServices();
        builder.Services.AddAppOptions(builder.Configuration);
        builder.Services.AddAwsServices(builder.Configuration);
        builder.Services.AddNotificationServices(builder.Configuration);
        builder.Services.AddRealtimeServices();

        builder.Services.AddCors();
        builder.Services.AddHttpClient();
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddAutoMapper(
            am => am.AddProfile<AutoMapperProfile>(),
            typeof(AutoMapperProfile).Assembly);

        builder.Services.AddControllers()
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

        builder.Services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        })
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.UseMiddleware<GlobalExceptionMiddleware>();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        app.UseCors(cors =>
        {
            cors
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .SetIsOriginAllowed(_ => true);
        });

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        app.MapHub<NamazTimeHub>("/hubs/namaz-time");

        foreach (var endpointSource in app.Services.GetServices<EndpointDataSource>())
        {
            foreach (var endpoint in endpointSource.Endpoints)
            {
                Console.WriteLine($"ENDPOINT: {endpoint.DisplayName}");
            }
        }

        app.MigrateDatabase(Log.ForContext<Program>());

        app.Run();
    }
}
