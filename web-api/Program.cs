using DataAccessLayer;
using Interfaces;
using LinqToDB;
using LinqToDB.AspNet;
using LinqToDB.AspNet.Logging;

namespace text_forum_web_app;
public class Program
{        
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var connec = builder.Configuration.GetConnectionString("Default");
        builder.Services.AddLinqToDBContext<DataBaseConnection>((provider, options)
            => options
                .UseSQLite(builder.Configuration.GetConnectionString("Default"))
                .UseDefaultLogging(provider));

        builder.Services.AddTransient<GenerateDatabase>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<ICommentService, CommentService>();
        builder.Services.AddScoped<IPostService, PostService>();
        builder.Services.AddScoped<IModeratorService, ModeratorService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
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


