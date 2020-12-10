using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalManagementSystem.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyHospitalManagement.Models;

namespace MyHospitalManagement
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			//services.AddCors();
			services.AddControllers();

			//services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			//services.AddDbContext<HospitalContext>(opts =>
			//opts.UseSqlServer("Server=ZERO;Database=Hospital;Trusted_Connection=True;"));
			//   opts.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Hospital;Trusted_Connection=True;"));

			services.AddDbContextPool<HospitalContext>(opts => opts.UseSqlServer(Configuration.GetConnectionString("EmployeeDbConnection")));


			services.AddScoped<IPatientsRepository, PatientsRepository>();
			services.AddScoped<IDoctorsRepository, DoctorsRepository>();
			services.AddScoped<INursesRepository, NursesRepository>();
			services.AddScoped<IRoomsRepository, RoomsRepository>();
			services.AddScoped<IUsersRepository, UsersRepository>();

			// In production, the React files will be served from this directory
			//services.AddSpaStaticFiles(configuration =>
			//{
			//	configuration.RootPath = "ClientApp/build";
			//});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}


			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
		//{
		//	if (env.IsDevelopment())
		//	{
		//		app.UseDeveloperExceptionPage();
		//	}
		//	else
		//	{
		//		app.UseExceptionHandler("/Error");
		//		app.UseHsts();
		//	}

		//	app.UseCors(
		//		options => options
		//			.AllowAnyOrigin()
		//			.AllowAnyHeader()
		//			.AllowAnyMethod()
		//	);

		//	app.UseHttpsRedirection();
		//	app.UseStaticFiles();
		//app.UseSpaStaticFiles();

		//app.UseMvc(routes =>
		//{
		//	routes.MapRoute(
		//		name: "default",
		//		template: "{controller}/{action=Index}/{id?}");
		//});

		//app.UseSpa(spa =>
		//{
		//	spa.Options.SourcePath = "ClientApp";

		//	if (env.IsDevelopment())
		//	{
		//		spa.UseReactDevelopmentServer(npmScript: "start");
		//	}
		//});
		//}
	}
}
