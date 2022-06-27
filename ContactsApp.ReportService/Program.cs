using Confluent.Kafka;
using ContactsApp.Core.Middlewares;
using ContactsApp.ReportService.Clients;
using ContactsApp.ReportService.Services;
using ContactsApp.ReportService.Settings;
using ContactsApp.ReportService.UnitOfWork;
using Microsoft.Extensions.FileProviders;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateBootstrapLogger();
try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog((context, services, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext());
    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddMongoDB();
    builder.Services.AddScoped<IReportUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped<UseExceptionHandlingMiddleware>();

    builder.Services.AddKafkaServices();

    builder.Services.AddHttpClient<ContactClient>(client =>
    {
        client.BaseAddress = new Uri("https://localhost:7297");
    });

    var app = builder.Build();
    app.UseSerilogRequestLogging(options =>
    {
        options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} ({UserId}) responded {StatusCode} in {Elapsed:0.0000}ms";
    });

    

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    
    app.UseMiddleware<UseExceptionHandlingMiddleware>();
    
    app.UseHttpsRedirection();

    app.UseAuthorization();
    
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(
            Path.Combine(builder.Environment.ContentRootPath, "Reports")),
        RequestPath = "/report"
    });
    
    app.MapControllers();
    
    

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}

return 0;