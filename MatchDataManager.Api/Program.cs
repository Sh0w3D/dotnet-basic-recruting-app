using MatchDataManager.Api;
using MatchDataManager.Application;

var builder = WebApplication.CreateBuilder(args);
{
    // add services to the container
    builder.Services
        .AddApiServices()
        .AddApplicationServices();
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.MapControllers();

    await app.RunAsync();
}
