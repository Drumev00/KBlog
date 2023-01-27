using Blog.Core.Exceptions;
using Blog.Infrastructure;
using Blog.Infrastructure.Seed;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Security.Authentication;

namespace Blog.Web.Extensions
{
	public static class ApplicationBuilderExtensions
	{
		public static void ApplyMigrations(this IApplicationBuilder app)
		{
			using var services = app.ApplicationServices.CreateScope();

			var dbContext = services.ServiceProvider.GetService<ApplicationDbContext>();


			dbContext!.Database.Migrate();

			new DbContextSeeder().SeedAsync(dbContext).GetAwaiter().GetResult();
		}

		public static void ExceptionHandlerConfigure(this IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.Run(async context =>
			{
				context.Response.ContentType = "application/json";
				IExceptionHandlerPathFeature? pathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
				Exception? ex = pathFeature?.Error;

				if (ex == null)
					return;

				context.Response.StatusCode = ex switch
				{
					ItemNotFoundException => StatusCodes.Status404NotFound,
					InvalidCredentialException or AuthenticationException => 441,
					_ => StatusCodes.Status500InternalServerError
				};

				object response = new
				{
					Message = ex.Message,
					StackTrace = ex.StackTrace,
					Path = pathFeature?.Path,
					InnerExceptionMessages = ex.GetInnerExceptionMessages()
				};

				if (!env.IsDevelopment())
					response = new { ex.Message };

				await context.Response.WriteAsJsonAsync(response);
			});
		}
	}
}
