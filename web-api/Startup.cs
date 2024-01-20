﻿using LinqToDB;
using LinqToDB.AspNet;
using LinqToDB.AspNet.Logging;
using DataAccessLayer;

namespace text_forum_web_app;
public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        //using LinqToDB.AspNet
        services.AddLinqToDBContext<DataBaseConnection>((provider, options)
            => options
                //will configure the AppDataConnection to use
                //sqite with the provided connection string
                //there are methods for each supported database
                .UseSQLite(Configuration.GetConnectionString("Default"))
                //default logging will log everything using the ILoggerFactory configured in the provider
                .UseDefaultLogging(provider));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var dataConnection = scope.ServiceProvider.GetService<DataBaseConnection>();
            //dataConnection.CreateTable<Person>();
        }
    }
}
