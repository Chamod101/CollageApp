using CollageApp.Configurations;
using CollageApp.Data;
using CollageApp.Data.Repository;
using CollageApp.Data.Repository.Interface;
using CollageApp.MyLogging;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMyLogger,LogtoFile>();
builder.Services.AddScoped<IStudentRepository,StudentRepository>();
builder.Services.AddScoped(typeof(ICommonRepository<>),typeof(CommonRepository<>));

builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

builder.Services.AddDbContext<CollegeDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CollageAppDbConnection"));
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });

    options.AddPolicy(name: "AllowOnlyLocalHost",
        policy =>
        {
            //Allow specific origin
            policy.WithOrigins("http://localhost:4200");
        });

    options.AddPolicy(name: "AllowOnlyGoogleApplications",
        policy =>
        {
            policy.WithOrigins("http://google.com").AllowAnyHeader().AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("api/GetAllStudents",
        context => context.Response.WriteAsync("Test Response"))
    .RequireCors("AllowOnlyLocalHost");

    endpoints.MapControllers();
});

app.MapControllers();

app.Run();
