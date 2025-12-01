using Microsoft.EntityFrameworkCore;
using RijschoolHarmonieApp.Data;
using RijschoolHarmonieApp.Repositories;
using RijschoolHarmonieApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddDbContext<RijschoolHarmonieAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder
    .Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new System.Text.Json.Serialization.JsonStringEnumConverter()
        );
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IInstructorPriceRepository, InstructorPriceRepository>();
builder.Services.AddScoped<IInstructorPriceService, InstructorPriceService>();
builder.Services.AddScoped<IStudentAccountRepository, StudentAccountRepository>();
builder.Services.AddScoped<IStudentAccountService, StudentAccountService>();

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "RijschoolHarmonie API V1");
    });
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
