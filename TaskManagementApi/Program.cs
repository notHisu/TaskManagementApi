using TaskManagementApi;
using TaskManagementApi.Interfaces;
using TaskManagementApi.Models;
using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Repositories;
using TaskManagementApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddSingleton<ITaskService, TaskService>();

builder.Services.AddTransient<IGenericRepository<TaskItem>, TaskRepository>();
builder.Services.AddTransient<IGenericRepository<TaskLabel>, TaskLabelRepository>();
builder.Services.AddTransient<IGenericRepository<TaskComment>, TaskCommentRepository>();
builder.Services.AddTransient<IGenericRepository<Category>, CategoryRepository>();
builder.Services.AddTransient<IUserRepository<UserResponseDto>, UserRepository>();

builder.Services.AddDbContext<TaskContext>(options =>

{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"] ?? throw new InvalidOperationException("JWT Secret not configured")))
        };
    });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<RequestLoggingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
