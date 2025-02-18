﻿using Repository;
using Database.Context;
using Database.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var cons = context.Configuration.GetConnectionString("DefaultConnection");
        services.AddTransient<ProductRepository>(); // 의존성 등록
        services.AddDbContextPool<ShopDbContext>(x => x.UseNpgsql(cons));
    })
    
    .Build();

var service = host.Services;
var context= service.GetRequiredService<ShopDbContext>();

context.ErrorLogs.Add(new ErrorLog
{
    
    Timestamp = DateTime.UtcNow,
    Message = "Error Message",
    ExceptionMessage = "Error Message",
    StackTrace = "Stack Trace"
});


await context.SaveChangesAsync();




Console.ReadLine();
