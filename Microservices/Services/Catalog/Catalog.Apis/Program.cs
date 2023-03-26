#region DI
using Catalog.Apis.Data;
using Catalog.Apis.Repository;
using Catalog.Apis.Shared;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalog.API", Version = "v1" });
});

// interface + class 
builder.Services.Configure<DatabaseSettings>(op=>builder.Configuration.GetSection("DatabaseSettings").Bind(op));

builder.Services.AddSingleton<ICatalogContext , CatalogContext>();
builder.Services.AddScoped<IProductRepository, ProductRepository>(); // Ioc Container (SOLID Principle)

#endregion




#region Middleware
var app = builder.Build();

// Configure the HTTP request pipeline.

var env = app.Environment;

if (env.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog.API v1"));
}

app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion