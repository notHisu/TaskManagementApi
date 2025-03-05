using TaskManagementApi.Interfaces;
using TaskManagementApi.Models;
using TaskManagementApi.DTOs;
using TaskManagementApi.Repositories;
using TaskManagementApi.Middlewares;
using TaskManagementApi.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Azure;
using System.Text;
using TaskManagementApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<ITaskRepository<TaskItem>, TaskRepository>();
builder.Services.AddTransient<ITaskLabelRepository<TaskLabel>, TaskLabelRepository>();
builder.Services.AddTransient<ITaskCommentRepository<TaskComment>, TaskCommentRepository>();
builder.Services.AddTransient<ICategoryRepository<Category>, CategoryRepository>();
builder.Services.AddTransient<ITaskAttachmentRepository, TaskAttachmentRepository>();

builder.Services.AddSingleton<IBlobStorageService, BlobStorageService>();

// Configure the database
builder.Services.AddDbContext<TaskContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Configure AutoMapper
builder.Services.AddAutoMapper(typeof(TaskMapper).Assembly);
builder.Services.AddAutoMapper(typeof(UserMapper).Assembly);
builder.Services.AddAutoMapper(typeof(CategoryMapper).Assembly);
builder.Services.AddAutoMapper(typeof(TaskLabelMapper).Assembly);
builder.Services.AddAutoMapper(typeof(TaskCommentMapper).Assembly);

// Register Identity with API endpoints
//builder.Services.AddIdentity<User, IdentityRole>()
//    .AddEntityFrameworkStores<TaskContext>()
//    .AddDefaultTokenProviders();

builder.Services.AddIdentityApiEndpoints<User>()
    .AddEntityFrameworkStores<TaskContext>();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", builder =>
    {
        builder.WithOrigins("https://localhost:7202")
               .AllowCredentials()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAzureClients(clientBuilder =>
{
    var storageConfig = builder.Configuration.GetSection("StorageConnection");
    clientBuilder.AddBlobServiceClient(builder.Configuration["blobServiceUri"]!).WithName("StorageConnection");
    clientBuilder.AddQueueServiceClient(builder.Configuration["queueServiceUri"]!).WithName("StorageConnection");
    clientBuilder.AddTableServiceClient(builder.Configuration["tableServiceUri"]!).WithName("StorageConnection");
});

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");

app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<JwtCookieMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

// Map Identity API
app.MapIdentityApi<User>();
app.MapControllers();

app.Run();
