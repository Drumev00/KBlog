using Blog.Core.Entities;
using Blog.Infrastructure;
using Blog.Infrastructure.Data.Services;
using Blog.Infrastructure.Data.Services.Rate.Articles;
using Blog.Infrastructure.Data.Services.Rate.Comments;
using Blog.Infrastructure.Data.Services.Rate.SubComments;
using Blog.Infrastructure.Data.Services.SendGrid;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;

namespace Blog.Web.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			return services
				.AddTransient<ImagesService>()
				.AddTransient<EmailSender>()
				.AddTransient<ArticlesService>()
				.AddTransient<CategoriesService>()
				.AddTransient<SubCategoriesService>()
				.AddTransient<ArticlesCategoriesService>()
				.AddTransient<ArticlesSubCategoriesService>()
				.AddTransient<ArticlesRateService>()
				.AddTransient<CommentsService>()
				.AddTransient<CommentsRateService>()
				.AddTransient<SubCommentsService>()
				.AddTransient<SubCommentsRateService>()
				.AddTransient<AuthenticationService>();
		}

		public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
		{
			services.AddSwaggerGen(swagger =>
			{
				swagger.SwaggerDoc("v1", new OpenApiInfo
				{
					Version = "v1",
					Title = "ASP.NET Core 6 Web API"
				});

				swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
				{
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer",
					BearerFormat = "JWT",
					In = ParameterLocation.Header,
					Description = $"Enter ‘Bearer’ [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\\"
				});

				swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							}
						},
						new string[] { }
					}
				});
			});

			return services;
		}

		public static IServiceCollection AddIdentityService(this IServiceCollection services)
		{
			services.AddIdentity<User, Role>(options =>
			{
				options.Password.RequiredLength = 6;
				options.Password.RequireDigit = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireUppercase = false;
				options.Password.RequireNonAlphanumeric = false;
				options.User.RequireUniqueEmail = true;
				options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+ ";
				options.SignIn.RequireConfirmedEmail = true;
			})
		.AddEntityFrameworkStores<ApplicationDbContext>()
		.AddDefaultTokenProviders();

			return services;
		}

		public static IServiceCollection AddFacebookAuth(this IServiceCollection services, IConfiguration configuration) 
		{
			services.AddAuthentication().AddFacebook(facebookOptions =>
			{
				facebookOptions.AppId = configuration["Authentication:Facebook:AppId"];
				facebookOptions.AppSecret = configuration["Authentication:Facebook:AppSecret"];
				facebookOptions.Fields.Add("picture");
				facebookOptions.Events = new OAuthEvents
				{
					OnCreatingTicket = context =>
					{
						var identity = (ClaimsIdentity)context.Principal.Identity;
						var profileImg = context.User.GetProperty("picture").GetProperty("data").GetProperty("url").ToString();
						identity.AddClaim(new Claim(ClaimTypes.UserData, profileImg));
						return Task.CompletedTask;
					}
				};
			});

			return services;
		}

		public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, TokenValidationParameters tokenValidationParameters)
		{
			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.SaveToken = true;
				options.RequireHttpsMetadata = false;
				options.TokenValidationParameters = tokenValidationParameters;
			});

			return services;
		}

		public static IServiceCollection ConfigureCors(this IServiceCollection services)
		{
			services.AddCors(options =>
			{
				options.AddPolicy("EnableCORS", builder =>
				{
					builder.WithOrigins("http://localhost:4200", "https://localhost:57295")
					.AllowAnyMethod()
					.AllowAnyHeader();
				});
			});

			return services;
		}
	}
}
