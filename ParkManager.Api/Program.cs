using ParkManager.Api;
using ParkManager.Api.Filters;
using ParkManager.Api.Middlewares;
using ParkManager.Api.Services;
using ParkManager.Application;
using ParkManager.Application.Contracts.Authentication;
using ParkManager.Persistence;
using ParkManager.Persistence.DataContexts;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(LoggingConfiguration.ConfigureLogger);
// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ILoggedInUserService, LoggedInUserService>();
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new GlobalExceptionFilter());
    options.ReturnHttpNotAcceptable = true;
}).AddXmlDataContractSerializerFormatters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//var optionsBuilder = new DbContextOptionsBuilder<ParkManagerDbContext>();
//optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
//o => o.CommandTimeout(180).ExecutionStrategy(c => new SqlServerRetryingExecutionStrategy(c)));

builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.ToString());
    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});
//builder.Services.AddStackExchangeRedisCache(option =>
//{
//    option.Configuration = builder.Configuration["RedisConnectionString"];
//    //option.ConfigurationOptions.AbortOnConnectFail = false;
//});
builder.Services.AddDistributedMemoryCache();
//builder.Services.AddApplicationInsightsTelemetry();
//builder.Services.AddHealthChecks().AddDbContextCheck<ParkManagerDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseMiddleware<GlobalErrorHandlerMiddleware>();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseSerilogRequestLogging();
app.Run();

public partial class Program { }