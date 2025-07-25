//using AutoMapper;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.OpenApi.Models;
//using SIGEBI.api.Services;
//using SIGEBI.Application.Contracts;
//using SIGEBI.Application.Contracts.Repositories.Reservations;
//using SIGEBI.Application.Contracts.Services;
//using SIGEBI.Application.Services;  
//using SIGEBI.Persistence.Context;
//using SIGEBI.Persistence.Logging;
//using SIGEBI.Persistence.Repositories;


//var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAll", policy =>
//    {
//        policy.AllowAnyOrigin()
//              .AllowAnyMethod()
//              .AllowAnyHeader();
//    });
//});

//// Add services to the container.

//builder.Services.AddDbContext<SIGEBIContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("SIGEBIConnection")));

//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); // Ensure AutoMapper is properly referenced (te amo copilot)


////Repositorios //
//builder.Services.AddScoped<IReservationStatusesRepository, ReservationStatusesRepository>();
//builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
//builder.Services.AddScoped<IReservationHistoryRepository, ReservationHistoryRepository>();


////Servicios
//builder.Services.AddTransient<IReservationService, ReservationService>();
//builder.Services.AddTransient<IReservationStatusService, ReservationStatusService>();
//builder.Services.AddTransient<IReservationHistoryService, ReservationHistoryService>();
//builder.Services.AddTransient<ReservationApiService>();


//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo
//    {
//        Title = "SIGEBI API",
//        Version = "v1",
//        Description = "API para el sistema de gestión de biblioteca SIGEBI",
//        Contact = new OpenApiContact
//        {
//            Name = "Soporte SIGEBI",
//            Email = "soporte@sigebi.com"
//        }
//    });
//});


//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI(c =>
//    {
//        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SIGEBI API V1");

//        c.DocumentTitle = "SIGEBI API Documentation";
//        c.RoutePrefix = string.Empty; 
//        c.EnableTryItOutByDefault(); // Habilita el botón "Try it out" por defecto
//    });
//}

//app.MapGet("/", () => Results.Redirect("/swagger"));

//app.UseHttpsRedirection();

//app.UseCors("AllowAll");

//app.UseAuthorization();

//app.MapControllers();

//app.Run();
