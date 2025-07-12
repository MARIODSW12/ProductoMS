using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using dotenv.net;
using DotNetEnv;
using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using Producto.Application.Commands.CommandHandler;
using Producto.Domain.Events;
using Producto.Domain.Events.EventHandler;
using Producto.Domain.Interfaces;
using Producto.Infrastructure.Configurations;
using Producto.Infrastructure.Consumers;
using Producto.Infrastructure.Interfaces;
using Producto.Infrastructure.Persistences.Repositories.MongoRead;
using Producto.Infrastructure.Persistences.Repositories.MongoWrite;
using Producto.Infrastructure.Queries.QueryHandler;
using Producto.Infrastructure.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar configuración de MongoDB
builder.Services.AddSingleton<MongoWriteProductoDbConfig>();
builder.Services.AddSingleton<MongoReadProductoDbConfig>();

// REGISTRA EL REPOSITORIO ANTES DE MediatR
builder.Services.AddScoped<IProductoRepository, ProductoWriteRepository>();
builder.Services.AddScoped<IProductoReadRepository, ProductoReadRepository>();
builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();

// REGISTRA MediatR PARA TODOS LOS HANDLERS
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetProductoPorIdQueryHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetProductosPorCategoriaQueryHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetProductosPorIdSubastadorQueryHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetTodosLosProductosQueryHandler).Assembly));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AgregarProductoCommandHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ActualizarProductoCommandHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(EliminarProductoCommandHandler).Assembly));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AgregarProductoEventHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ActualizarProductoEventHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(EliminarProductoEventHandler).Assembly));

//builder.Services.AddValidatorsFromAssemblyContaining<CreateUserDtoValidation>();

builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.AddConsumer<AgregarProductoConsumer>();
    busConfigurator.AddConsumer<ActualizarProductoConsumer>();
    busConfigurator.AddConsumer<EliminarProductoConsumer>();

    busConfigurator.SetKebabCaseEndpointNameFormatter();
    busConfigurator.UsingRabbitMq((context, configurator) =>
    {
        configurator.Host(new Uri(Environment.GetEnvironmentVariable("RABBIT_URL")), h =>
        {
            h.Username(Environment.GetEnvironmentVariable("RABBIT_USERNAME"));
            h.Password(Environment.GetEnvironmentVariable("RABBIT_PASSWORD"));
        });

        configurator.ReceiveEndpoint(Environment.GetEnvironmentVariable("RABBIT_QUEUE_AgregarProducto"), e => {
            e.ConfigureConsumer<AgregarProductoConsumer>(context);
        });

        configurator.ReceiveEndpoint(Environment.GetEnvironmentVariable("RABBIT_QUEUE_ActualizarProducto"), e => {
            e.ConfigureConsumer<ActualizarProductoConsumer>(context);
        });

        configurator.ReceiveEndpoint(Environment.GetEnvironmentVariable("RABBIT_QUEUE_EliminarProducto"), e => {
            e.ConfigureConsumer<EliminarProductoConsumer>(context);
        });

        configurator.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5)));
        configurator.ConfigureEndpoints(context);
    });
});
EndpointConvention.Map<AgregarProductoEvent>(new Uri("queue:" + Environment.GetEnvironmentVariable("RABBIT_QUEUE_AgregarProducto")));
EndpointConvention.Map<ActualizarProductoEvent>(new Uri("queue:" + Environment.GetEnvironmentVariable("RABBIT_QUEUE_ActualizarProducto")));
EndpointConvention.Map<EliminarProductoEvent>(new Uri("queue:" + Environment.GetEnvironmentVariable("RABBIT_QUEUE_EliminarProducto")));

// Configuración CORS permisiva
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()  // Permite cualquier dominio
            .AllowAnyMethod()  // GET, POST, PUT, DELETE, etc.
            .AllowAnyHeader(); // Cualquier cabecera
    });
});

var app = builder.Build();

// Habilitar CORS
app.UseCors("AllowAll");

//app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.
/*app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Productos API v1");
    c.RoutePrefix = string.Empty;
});*/

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
