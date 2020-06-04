using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Core.Models.Security;

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
			});
			AuthenticationSetup(services);

			services.AddControllersWithViews();
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
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
