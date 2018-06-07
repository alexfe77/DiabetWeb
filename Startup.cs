using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using DiabetWeb.Services;
using DiabetWeb.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace DiabetWeb
{
	public class Startup
	{
		private IConfiguration _configuration;

		public Startup(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddAuthentication(options =>
			{
				options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
			})
			.AddOpenIdConnect(options =>
			{
				_configuration.Bind("AzureAD", options);
			})
			.AddCookie();

			services.AddSingleton<IGreeter, Greeter>();

			//services.AddDbContext<DiabetWebDbContext>(options => options.UseSqlServer(_configuration.GetConnectionString("DiabetWeb")));
			services.AddDbContext<DiabetWebMemoryContext>(options => options.UseInMemoryDatabase("DiabetWebMemory"));
			//services.AddScoped<IDiabetWebData, SqlDiabetWebData>();
			services.AddScoped<IDiabetWebData, InMemoryDiabetWebData>();
			services.AddMvc()
				.AddMvcOptions(o => o.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter()));
				//.AddJsonOptions(o =>
				//{
				//	if (o.SerializerSettings.ContractResolver != null)
				//	{
				//		var castedResolver = o.SerializerSettings.ContractResolver as DefaultContractResolver;
				//		castedResolver.NamingStrategy = null;
				//	}
				//});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(
			IGreeter greeter,
			IApplicationBuilder app,
			IHostingEnvironment env,
			ILogger<Startup> logger)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRewriter(new RewriteOptions().AddRedirectToHttpsPermanent());

			app.UseStaticFiles();

			app.UseNodeModules(env.ContentRootPath);
			app.UseScripts(env.ContentRootPath);
			app.UseData(env.ContentRootPath);

			app.UseAuthentication();

			app.UseMvc(ConfigureRoutes);

		}

		private void ConfigureRoutes(IRouteBuilder routeBuilder)
		{
			routeBuilder.MapRoute("Default", "{controller=Home}/{action=DiabetCalc}");
		}
	}
}
