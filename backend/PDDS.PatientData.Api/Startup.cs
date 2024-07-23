using Microsoft.AspNetCore.Authentication;
using PDDS.PatientData.Core.Helpers;
using PDDS.PatientData.Core.JWT;
using PDDS.PatientData.Core.Entities;
using PDDS.PatientData.Core.Services;
using PDDS.PatientData.Data;
using PDDS.PatientData.Core.Repositories;
using PDDS.PatientData.Services;
using PDDSAuthenticationService = PDDS.PatientData.Services.AuthenticationService;
using Microsoft.EntityFrameworkCore;

namespace PDDS.PatientData.Api
{
    public class Startup
    {
        public IConfiguration config { get; }

        public Startup(IConfiguration configuration)
        {
            config = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PatientDataContext>(options =>
                    options.UseSqlite(config.GetConnectionString("PatientDataContext")));

            services.AddCors(opt => opt.AddPolicy("CorsPolicy", c =>
            {
                c.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            }));


            services.AddControllers()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddMvc()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                });

            services.AddMvcCore().AddNewtonsoftJson();

            services.Configure<AppSettings>(config.GetSection("AppSettings"));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IAuthenticationService<User, int>, PDDSAuthenticationService>();

            services.AddScoped<IPatientRepository<Patient, int>, PatientRepository>();
            services.AddScoped<IPatientService<Patient, int>, PatientService>();

            services.AddScoped<IUserRepository<User, int>, UserRepository>();
            services.AddScoped<IUserService<User, int>, UserService>();

            services.AddHttpContextAccessor();

            services.AddSingleton<IUriService>(o =>
            {
                var accessor = o.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(uri);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            DatabaseHelper.InitializeDatabase();
        }
    }
}
