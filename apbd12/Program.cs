using apbd12.Data;
using apbd12.Repositories;
using apbd12.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<ITripsService, TripsService>();
builder.Services.AddScoped<ITripsRepository, TripsRepository>();
builder.Services.AddScoped<IClientsRepository, ClientsRepository>();
builder.Services.AddScoped<IClientsService, ClientsService>();


builder.Services.AddDbContext<TripContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
);



var app = builder.Build();

// Configure the HTTP request pipeline.
//app.UseAuthorization();

app.MapControllers();

app.Run();