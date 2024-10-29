using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IGenericDal<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUserDal, EfUserRepository>();
builder.Services.AddScoped<IUserService, UserManager>();
builder.Services.AddScoped<IAuthService, AuthManager>();
builder.Services.AddScoped<IRequestService, RequestManager>();
builder.Services.AddScoped<IRequestDal, EfRequestRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularClient",
        builder => builder.WithOrigins("http://localhost:4200")
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAngularClient");

app.UseAuthorization();

app.MapControllers();

// Initialize the database and create admin user
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<Context>();
    var userManager = services.GetRequiredService<IUserService>();

    context.Database.Migrate();
    SeedUsers(userManager).Wait();
}

app.Run();

async Task SeedUsers(IUserService userManager)
{
    var users = new List<User>
    {
        new User
        {
            Username = "admin",
            Email = "admin@gmail.com",
            Password = BCrypt.Net.BCrypt.HashPassword("admin123"),
            Role = Role.Admin
        },
        new User
        {
            Username = "teamleader",
            Email = "teamleader@gmail.com",
            Password = BCrypt.Net.BCrypt.HashPassword("teamleader123"),
            Role = Role.TeamLeader
        },
        new User
        {
            Username = "director",
            Email = "director@gmail.com",
            Password = BCrypt.Net.BCrypt.HashPassword("director123"),
            Role = Role.Director
        }
    };

    foreach (var user in users)
    {
        var existingUser = await userManager.GetUserByEmail(user.Email);
        if (existingUser == null)
        {
            await userManager.AddUser(user);
        }
    }
}

