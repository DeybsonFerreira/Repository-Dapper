using Microsoft.AspNetCore.Mvc;
using repository_dapper.Models;
using repository_dapper.Repository;

var builder = WebApplication.CreateBuilder(args);

//swagger
builder.Services.AddMvc();

builder.Services.AddEndpointsApiExplorer();//add for swagger
builder.Services.AddSwaggerGen();//add for swagger


//dependence injection
builder.Services.AddScoped<ProductRepositoryDapper>(x => new ProductRepositoryDapper(builder.Configuration));

var app = builder.Build();

//swagger middleware
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MY API");
});

app.MapGet("/Product", async ([FromServices] ProductRepositoryDapper repository) =>
{
    return Results.Ok(await repository.GetAllAsync());
});

app.MapGet("/Product/{id}", async ([FromRoute] int id, [FromServices] ProductRepositoryDapper repository) =>
{
    return Results.Ok(await repository.GetByIdAsync(id));
});

app.MapPost("/Product", async ([FromBody] Product model, [FromServices] ProductRepositoryDapper repository) =>
{
    await repository.AddAsync(model);
    return Results.Created($"/products/{model.Id}", model.Id);
});
app.Run();
