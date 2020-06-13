using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models.Security;
using Core.Models.SystemModels;
using Core.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDBRepository;
using System;

namespace WebApplication
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration) => Configuration = configuration;

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc(options =>
			{
				options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
				var policy = new AuthorizationPolicyBuilder()
				.RequireAuthenticatedUser()
				.Build();
				options.Filters.Add(new AuthorizeFilter(policy));
			}).AddRazorRuntimeCompilation();
			services.AddAntiforgery(o => o.HeaderName = "CSRF-TOKEN");
			services.AddCors();
			AuthenticationSetup(services);

			AddRepositories(services);
			AddServices(services);

			services.AddControllersWithViews();
		}
		private void AddServices(IServiceCollection services)
		{
			services.AddSingleton(sp =>
			{
				return new DatabaseSettings
				{
					ConnectionString = "mongodb://localhost:27017/",
					DatabaseName = "Flower"
				};
			});

			services.AddSingleton<IGroupService, GroupService>();
			services.AddSingleton<IUserService, UserService>();
			services.AddSingleton<IRoleService, RoleService>();
			services.AddSingleton<IUserRoleService, UserRoleService>();
			services.AddSingleton<IInitialDataService, InitialDataService>();
			services.AddSingleton<IRoleMenuService, RoleMenuService>();
			services.AddSingleton<IMenuService, MenuService>();
		}

		private void AddRepositories(IServiceCollection services)
		{
			services.AddSingleton<IGroupRepository, GroupRepository>();
			services.AddSingleton<IRoleRepository, RoleRepository>();
			services.AddSingleton<IUserRepository, UserRepository>();
			services.AddSingleton<IUserRoleRepository, UserRoleRepository>();
			services.AddSingleton<IMenuRepository, MenuRepository>();
			services.AddSingleton<IRoleMenuRepository, RoleMenuRepository>();
		}

		private void AuthenticationSetup(IServiceCollection services)
		{
			var authenticationSettings = new AuthenticationSettings();
			Configuration.Bind("AuthenticationSettings", authenticationSettings);

			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(options =>
				{
					options.LogoutPath = authenticationSettings.LogoutPath;
					options.LoginPath = authenticationSettings.LoginPath;
					options.AccessDeniedPath = authenticationSettings.AccessDeniedPath;
				}
				);

			services.AddDataProtection()
				.SetDefaultKeyLifetime(TimeSpan.FromDays(15))
				.SetApplicationName("Flower")
				.DisableAutomaticKeyGeneration();
			services.Configure<IdentityOptions>(options =>
			{
				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
				options.Lockout.MaxFailedAccessAttempts = 3;
				options.Lockout.AllowedForNewUsers = true;
			});

			services.AddTransient(sp =>
			{
				return new AuthenticationProperties
				{
					AllowRefresh = authenticationSettings.AllowRefresh,
					ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(authenticationSettings.ExpireMinutes),
					IsPersistent = authenticationSettings.IsPersistent,
					IssuedUtc = DateTimeOffset.UtcNow
				};
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			var sp = app.ApplicationServices;
			(sp.GetService(typeof(IInitialDataService)) as IInitialDataService).Init();

			app.UseStaticFiles();
			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseCors(option => option
			   .AllowAnyOrigin()
			   .AllowAnyMethod()
			   .AllowAnyHeader());

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "Areas",
					pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
