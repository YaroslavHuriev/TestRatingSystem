using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

using TestRatingSystem.Authentication;
using TestRatingSystem.Models;

namespace TestRatingSystem {
	public class Startup {
		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {
			string con = "Server=(localdb)\\mssqllocaldb;Database=usersdbstore;Trusted_Connection=True;";
			// ????????????? ???????? ??????
			services.AddDbContext<SubmissionsContext>(options => options.UseSqlServer(con));
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
					.AddJwtBearer(options => {
						options.RequireHttpsMetadata = false;
						options.TokenValidationParameters = new TokenValidationParameters {
							// ????????, ????? ?? ?????????????? ???????? ??? ????????? ??????
							ValidateIssuer = true,
							// ??????, ?????????????? ????????
							ValidIssuer = AuthOptions.ISSUER,

							// ????? ?? ?????????????? ??????????? ??????
							ValidateAudience = true,
							// ????????? ??????????? ??????
							ValidAudience = AuthOptions.AUDIENCE,
							// ????? ?? ?????????????? ????? ?????????????
							ValidateLifetime = true,

							// ????????? ????? ????????????
							IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
							// ????????? ????? ????????????
							ValidateIssuerSigningKey = true,
						};
					});
			services.AddSwaggerGen();
			services.AddCors();
			services.AddSwaggerDocument();
			services.AddControllers();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			//app.UseHttpsRedirection();
			app.UseOpenApi();
			app.UseSwaggerUi3();
			app.UseCors(x => x
					.AllowAnyMethod()
					.AllowAnyHeader()
					.SetIsOriginAllowed(origin => true)//
					.AllowCredentials());
			app.UseRouting();

			app.UseAuthentication();

			app.UseAuthorization();

			app.UseEndpoints(endpoints => {
				endpoints.MapControllers();
			});
		}
	}
}
