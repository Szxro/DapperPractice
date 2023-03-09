using DapperPractice.Connection;
using DapperPractice.MiddleWares;
using DapperPractice.Repositories.MoviesRepository;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.
    builder.Services.AddControllers();
    builder.Services.AddScoped<IMovieRepository, MoviesRepository>();
    builder.Services.AddTransient<ISqlConnectionFactory, SqlConnectionFactory>();
    //MiddleWare
    builder.Services.AddTransient<GlobalErrorHandlingMiddleWare>(); // Have to register the server because is implementing an interface
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseMiddleware<GlobalErrorHandlingMiddleWare>();//This is use to do a global try/catch app

    app.MapControllers();

    app.Run();
}
