using System.Reflection;
using AuthorizationService.Application.Consumers;
using AuthorizationService.Application.Validators;
using AuthorizationService.Domain.Interfaces;
using AuthorizationService.Infrastructure.Context;
using AuthorizationService.Infrastructure.Repositories;
using AuthorizationService.Infrastructure.Services;
using BuildingBlocks.Behaviors;
using BuildingBlocks.Events;
using BuildingBlocks.ExceptionHandlers;
using FluentValidation;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddTransient<IPasswordHasher, PasswordHasher>();

builder.Services.AddCustomExceptionHandling();
builder.Services.AddProblemDetails();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(typeof(SignUpCommandValidator).Assembly);

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<PatientProfileUpdatedEventConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        cfg.ConfigureEndpoints(context);
    });
});

builder.Logging.AddConsole();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseExceptionHandler();
// app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();


app.Run();