global using FastEndpoints;
global using FastEndpoints.Swagger;
global using FluentValidation;
using DateOnlyTimeOnly.AspNet.Converters;
using InternLog.Api.Extensions;
using InternLog.Api.Options;
using System.Text.Json;
using Microsoft.AspNetCore.Cors.Infrastructure;
using InternLog.Api.Converters;
using static IdentityServer4.IdentityServerConstants;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.InstallServicesFromAssembly(builder.Configuration);
var app = builder.Build();

var swaggerOptions = new SwaggerOptions();
builder.Configuration.Bind(nameof(SwaggerOptions), swaggerOptions);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
}
else
{
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseCors(builder => builder.SetIsOriginAllowed((origin) => origin == "http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials());
app.UseAuthentication();
app.UseAuthorization();

app.UseFastEndpoints(config =>
{
	config.SerializerOptions = options =>
	{
		options.Converters.Add(new CustomDateOnlyJsonConverter());
		options.Converters.Add(new TimeOnlyJsonConverter());
	};
	config.GlobalEndpointOptions = (endpoint, builder) =>
	{
		builder.RequireCors(x => x.SetIsOriginAllowed((origin) => origin == "http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials()) // add this produce error
			.ProducesProblem(StatusCodes.Status403Forbidden);
	};
});

app.UseOpenApi();
app.UseSwaggerUi3(s => s.ConfigureDefaults());

app.Run();