using TodoApi.Models;
using TodoApi.Services;
using TodoApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<TodoDatabaseSettings>(builder.Configuration.GetSection("TodoDatabase"));
builder.Services.AddScoped<ITodoService, TodoService>();
builder.Services.AddSingleton<TodoService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI();
// }
// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
