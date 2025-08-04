using ProbabilityCalculator.API.Services;
using Scalar.AspNetCore;
using System.Text.Json.Serialization;

namespace ProbabilityCalculator.API
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;

		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddCors(options =>
			{
				options.AddPolicy("AllowLocalhost3000", builder =>
				{
					builder.WithOrigins("http://localhost:3000")
						   .AllowAnyMethod()
						   .AllowAnyHeader();
				});
			});

			services.AddControllers()
				.AddJsonOptions(options =>
				{
					options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
				});

			services.AddEndpointsApiExplorer();
			services.AddOpenApi();

			services.AddTransient<IProbabilityCalculatorService, ProbabilityCalculatorService>();
			services.AddTransient<ICalculationLogger, FileCalculationLogger>();

		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseRouting();

			app.UseCors("AllowLocalhost3000");

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();

				if (env.IsDevelopment())
				{
					endpoints.MapOpenApi();
					endpoints.MapScalarApiReference();
				}				
			});		

		}
	}
}
