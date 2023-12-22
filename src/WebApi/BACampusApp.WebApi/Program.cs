using BACampusApp.Authentication.Options;
using BACampusApp.Business.Abstracts;
using BACampusApp.Business.Extensions;
using BACampusApp.DataAccess.Contexts;
using BACampusApp.DataAccess.EFCore.Extensions;
using BACampusApp.DataAccess.EFCore.Repositories;
using BACampusApp.DataAccess.Extentesions;
using BACampusApp.DataAccess.Interfaces.Repositories;
using BACampusApp.WebApi;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));
builder.Services.Configure<ForgotPasswordEmailOptions>(builder.Configuration.GetSection("ForgotPasswordEmailSettings"));
builder.Services.AddDataAccessServices(builder.Configuration).AddEFCoreServices().AddBusinessServices();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// Printing Swagger Token.
builder.Services.AddSwaggerGen(setup =>
{
	// Include 'SecurityScheme' to use JWT Authentication
	var jwtSecurityScheme = new OpenApiSecurityScheme
	{
		BearerFormat = "JWT",
		Name = "JWT Authentication",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.Http,
		Scheme = JwtBearerDefaults.AuthenticationScheme,
		Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

		Reference = new OpenApiReference
		{
			Id = JwtBearerDefaults.AuthenticationScheme,
			Type = ReferenceType.SecurityScheme
		}
	};

	setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
	setup.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{ jwtSecurityScheme, Array.Empty<string>() }
	});
	setup.OperationFilter<CustomHeaderSwaggerAttribute>();
});
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddFluentValidationWithAssemblies();

builder.Services.AddHttpContextAccessor();

// CORS policy için frontend ekibinin host numaralarýnýn ekleneceði yer
var myOrigins = "_myOrigins";
builder.Services.AddCors(options =>
{
	options.AddPolicy(name: myOrigins,
		policy =>
		{
			policy.WithOrigins("*")
			.WithMethods("GET", "POST", "PUT", "HEAD", "DELETE", "CONNECT", "OPTIONS", "PATCH", "SEARCH")
			.AllowAnyHeader();
		});
});

var app = builder.Build();

// CORS policy aktif edildiði yer
app.UseCors(myOrigins);

// Custom middleware
app.Use(async (context, next) =>
{
	// Token'ý al
	var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

	// Token varsa, TokenBlacklist tablosunda kontrol et
	if (!string.IsNullOrEmpty(token))
	{
		var dbContext = context.RequestServices.GetService<BACampusAppDbContext>();
		var isTokenBlacklisted = await dbContext.TokenBlackLists.AnyAsync(t => t.Token == token);

		if (isTokenBlacklisted)
		{
			context.Response.StatusCode = 401; // Unauthorized
			return;
		}
	}

	await next.Invoke();
});




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseHttpsRedirection();

var locOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(locOptions.Value);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
