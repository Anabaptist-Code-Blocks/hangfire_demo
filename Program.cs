using Hangfire;
using Hangfire.PostgreSql;
using HangfireDemo;


var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;


builder.Services.AddHangfire(config =>
    config.UsePostgreSqlStorage(c =>
        c.UseNpgsqlConnection(configuration.GetConnectionString("HangfireConnection"))));


//Server where you don't set any options will use a "default" queue and use 20 workers.
builder.Services.AddHangfireServer();



//builder.Services.AddHangfireServer(options =>
//{
//    options.Queues = ["Vehicles", "TestQueue"];
//    options.ServerName = "MySecondServer";
//    options.WorkerCount = 5;
//});




builder.Services.AddScoped<BackgroundJobService>();

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>()
    .AddInMemorySubscriptions();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseRouting();

app.UseHangfireDashboard();

app.UseWebSockets();
app.MapGraphQL();

app.Run();


