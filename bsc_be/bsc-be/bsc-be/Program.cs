using bsc_be.Middlewares;
using bsc_be.Models;
using bsc_be.Repositories;
using bsc_be.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
var connStr = config.GetConnectionString("BSC_DB");


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IGigService, GigService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IPaymentMethodService, PaymentMethodService>();

builder.Services.AddDbContext<BscDbContext>(
    opt => opt.UseNpgsql(connStr)
);

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

var app = builder.Build();
app.UseMiddleware<GlobalExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
