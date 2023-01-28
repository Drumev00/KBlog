using Microsoft.EntityFrameworkCore;
using Blog.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Blog.Web.Extensions;
using Blog.Infrastructure.Data.Services.SendGrid;
using Serilog;

try
{
	var builder = WebApplication.CreateBuilder(args);

	// Add services to the container.

	var configuration = builder.Configuration;

	Log.Logger = new LoggerConfiguration()
		.MinimumLevel.Information()
		.WriteTo.Console()
		.WriteTo.Seq(configuration["Serilog:Seq:Url"])
		.CreateBootstrapLogger();

	builder.Host.UseSerilog();

	Log.Information("Starting up...");
	builder.Services.AddControllers();

	builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(
						configuration.GetConnectionString("Default")))
					.AddApplicationServices()
					.Configure<AuthMessageSenderOptions>(configuration)
					.AddSwaggerConfig()
					.AddIdentityService()
					.AddFacebookAuth(configuration);

	var tokenValidationParams = new TokenValidationParameters()
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidAudience = configuration["JWT:ValidAudience"],
		ValidIssuer = configuration["JWT:ValidIssuer"],
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
	};

	builder.Services.AddSingleton(tokenValidationParams);

	var app = builder.Build();

	//// Configure the HTTP request pipeline.
	//if (!app.Environment.IsDevelopment())
	//{
	//	app.UseExceptionHandler("/Home/Error");
	//	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	//	app.UseHsts();

	//}

	app.UseExceptionHandler(ab => ab.ExceptionHandlerConfigure(app.Environment));
	app.ApplyMigrations();
	app.UseHttpsRedirection();

	app.UseRouting();
	app.UseStaticFiles();

	app.UseSwagger();
	app.UseSwaggerUI(options =>
	{
		options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
		options.RoutePrefix = string.Empty;
	});

	app.UseAuthentication();
	app.UseAuthorization();

	app.MapControllerRoute(
		name: "default",
		pattern: "{controller=Home}/{action=Index}/{id?}");

	await app.RunAsync();

}
catch (Exception ex)
{
	Log.Fatal(ex, "Application accidentally crashed!");
	throw;
}
finally
{
	Log.CloseAndFlush();
}

