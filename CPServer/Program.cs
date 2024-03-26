using CPServer.Domain.Context;
using CPServer.Domain.Services.Impl;
using CPServer.Domain.Services.Interfaces;
using CPServer.GrpcServices;
using CPServer.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc().AddJsonTranscoding();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("DataSource=CPDatabase.db"));

builder.Services.AddTransient<ICustomerDataService, CustomerDataService>();
builder.Services.AddTransient<IAccountDataService, AccountDataService>();
builder.Services.AddTransient<ITransactionDataService, TransactionDataService>();
builder.Services.AddTransient<IDbSeed, DbSeed>();

var app = builder.Build();

SeedInitialDbData();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();

app.MapGrpcService<AccountGrpcService>();
app.MapGrpcService<TransactionGrpcService>();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();


void SeedInitialDbData() 
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbSeed>();
        dbInitializer.Initialize();
    }
}