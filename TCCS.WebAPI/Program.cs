using Microsoft.EntityFrameworkCore;
using TCCS.Application;
using TCCS.Application.Interfaces;
using TCCS.Application.Services;
using TCCS.DataAccess.Interfaces;
using TCCS.DataAccess.Models;
using TCCS.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<TccsContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("TccsConnection")));

builder.Services.ConfigureApplication(builder.Configuration);

builder.Services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

builder.Services.AddScoped<IEmployeeService, EmployeeService>();

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
