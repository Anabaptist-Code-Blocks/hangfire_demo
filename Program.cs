using Hangfire;
using Hangfire.PostgreSql;
using HangfireDemo;


var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddHangfire(config =>
    config.UsePostgreSqlStorage(c =>
        c.UseNpgsqlConnection(configuration.GetConnectionString("HangfireConnection"))));

builder.Services.AddHangfireServer();

builder.Services.AddScoped<VehicleService>();
builder.Services.AddScoped<SimulatedApisService>();

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseRouting();

app.UseHangfireDashboard();

app.MapGraphQL();

app.Run();


