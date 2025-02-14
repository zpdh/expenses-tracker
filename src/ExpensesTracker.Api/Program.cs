using ExpensesTracker.Api.Accessors;
using ExpensesTracker.Api.ExceptionHandler;
using ExpensesTracker.Api.OptionsSetup;
using ExpensesTracker.Application;
using ExpensesTracker.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddControllers();

builder.Services.AddRouting(options => {
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});

builder.Services.AddCors(options => {
    options.AddPolicy("TestingPolicy",
        policy => {
            policy.WithOrigins("http://localhost:4200/")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IUserAccessor, UserAccessor>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
builder.Services.AddAuthorization();

builder.Services
    .ConfigureOptions<JwtOptionsSetup>()
    .ConfigureOptions<JwtBearerOptionsSetup>();

var configuration = builder.Configuration;

builder.Services
    .AddInfrastructure(configuration)
    .AddApplication(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(_ => { });

app.UseCors("TestingPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();

public partial class Program
{
    protected Program()
    {

    }
}