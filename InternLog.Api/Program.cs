using InternLog.Api.Installers;
using InternLog.Api.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.InstallServicesFromAssembly(builder.Configuration);
var app = builder.Build();


var swaggerOptions = new SwaggerOptions();
ConfigurationBinder.Bind(builder.Configuration, nameof(SwaggerOptions), swaggerOptions);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(option => 
    {
        option.RouteTemplate = swaggerOptions.JsonRoute;
    });
    app.UseSwaggerUI(option =>
    {
        option.SwaggerEndpoint(swaggerOptions.UiEndpoint, swaggerOptions.Description);
    });
    app.UseMigrationsEndPoint();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
