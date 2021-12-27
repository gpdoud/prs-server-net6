using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using prs_server_net6.Middleware;
using prs_server_net6.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();

var connStrKey = "AppDbContext";
#if DEBUG
connStrKey += Environment.OSVersion.Platform == PlatformID.Win32NT ? "Win" : "Mac";
#endif

builder.Services.AddDbContext<AppDbContext>(x => {
    x.UseSqlServer(builder.Configuration.GetConnectionString(connStrKey));
});

var AppAccess = "_AppAccess";

builder.Services.AddCors(x => {
    x.AddPolicy(name: AppAccess,
        builder => {
            builder.WithOrigins("http://localhost", "http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
        }
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors(AppAccess);

app.UseAuthorization();

app.UseMiddleware<ApiKeyMiddleware>();

app.MapControllers();

using(var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope()) {
    scope.ServiceProvider.GetService<AppDbContext>().Database.Migrate();
}

app.Run();
