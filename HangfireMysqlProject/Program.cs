using Hangfire;
using Hangfire.MySql.Core;
using HangfireMysqlProject;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

ConfigurationManager configuration = builder.Configuration;

string hangfireConnectionString = configuration.GetConnectionString("HangfireDb");
builder.Services.AddHangfire(configuration => configuration
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseStorage(
                        new MySqlStorage(hangfireConnectionString,
                            new MySqlStorageOptions
                            {
                                QueuePollInterval = TimeSpan.FromSeconds(5),
                                JobExpirationCheckInterval = TimeSpan.FromSeconds(5),
                                CountersAggregateInterval = TimeSpan.FromMinutes(5),
                                PrepareSchemaIfNecessary = true,
                                DashboardJobListLimit = 25_000,
                                TransactionTimeout = TimeSpan.FromMinutes(1),
                                TablePrefix = "Hangfire"
                            }
                        )
                     ));

builder.Services.AddHangfireServer(options => options.WorkerCount = 1);

builder.Services.AddScoped<IJobTestService, JobTestService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.UseHangfireDashboard();

app.Run();
