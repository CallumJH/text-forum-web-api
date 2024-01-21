using System.Text;
using DataAccessLayer;
using Interfaces;
using LinqToDB;
using LinqToDB.AspNet;
using LinqToDB.AspNet.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace text_forum_web_app;
public class Program
{        
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.SaveToken = true;
            x.RequireHttpsMetadata = false;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidAudience = builder.Configuration["Jwt:Audience"],
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
            };
        });
        builder.Services.AddAuthorization(
            options =>
            {
                options.AddPolicy("Moderator", policy => policy.RequireClaim("IsModerator", "true"));
            }
        );
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        // Adding in Swagger bearer token authentication support.
        builder.Services.AddSwaggerGen(opt => {
            opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Text Forum API", Version = "v1" });
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });
            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    new string[] {}
                }
            });
        });

        var connec = builder.Configuration.GetConnectionString("Default");
        builder.Services.AddLinqToDBContext<DataBaseConnection>((provider, options)
            => options
                .UseSQLite(builder.Configuration.GetConnectionString("Default")!)
                .UseDefaultLogging(provider));

        builder.Services.AddTransient<GenerateDatabase>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<ICommentService, CommentService>();
        builder.Services.AddScoped<IPostService, PostService>();
        builder.Services.AddScoped<IModeratorService, ModeratorService>();
        builder.Services.AddScoped<IIdentityService, IdentityService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        // Use dependency injection to get GenerateDatabase and run the database initialization logic
        using (var scope = app.Services.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            var generateDB = serviceProvider.GetRequiredService<GenerateDatabase>();
            generateDB.CreateDataBaseTables();
        }

        app.Run();
    }
}


