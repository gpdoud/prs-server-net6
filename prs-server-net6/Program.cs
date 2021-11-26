using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using prs_server_net6.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var AppAccess = "_AppAccess";

builder.Services.AddControllers();

var connStrKey = "AppDbContext";
#if DEBUG
if(Environment.OSVersion.Platform != PlatformID.Win32NT) {
    connStrKey += "Mac";
}
#endif

builder.Services.AddDbContext<AppDbContext>(x => {
    x.UseSqlServer(builder.Configuration.GetConnectionString(connStrKey));
});

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

app.MapControllers();

app.Run();

