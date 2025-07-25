using Microsoft.EntityFrameworkCore;
using SIGEBI.api.Services;
using SIGEBI.Application.Contracts;
using SIGEBI.Application.Contracts.Repositories.Reservations;
using SIGEBI.Application.Contracts.Services;
using SIGEBI.Application.Services;
using SIGEBI.Persistence.Context;
using SIGEBI.Persistence.Logging;
using SIGEBI.Persistence.Repositories;
using SIGEBI.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<SIGEBIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SIGEBIConnection"),
    sqlOptions => sqlOptions.EnableRetryOnFailure()
    ));

builder.Services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

//Los repositorios
builder.Services.AddScoped<IReservationStatusesRepository, ReservationStatusesRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IReservationHistoryRepository, ReservationHistoryRepository>();

//Servicios

builder.Services.AddTransient<IReservationService, ReservationService>();
builder.Services.AddTransient<IReservationStatusService, ReservationStatusService>();
builder.Services.AddTransient<IReservationHistoryService, ReservationHistoryService>();
builder.Services.AddTransient<IBookService, FakeBookService>();

builder.Services.AddScoped<ReservationApiService>();
builder.Services.AddScoped<ReservationHistoryApiService>();
builder.Services.AddScoped<ReservationStatusApiService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
