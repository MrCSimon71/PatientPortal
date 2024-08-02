using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using PDDS.PatientData.Core.Helpers;
using PDDS.PatientData.Core.Entities;
using PDDS.PatientData.Core.Services;
using PDDS.PatientData.Core.Middleware;
using PDDS.PatientData.Core.Middleware.JWT;
using PDDS.PatientData.Core.Repositories;
using PDDS.PatientData.Data;
using PDDS.PatientData.Services;
using PDDSAuthenticationService = PDDS.PatientData.Services.AuthenticationService;

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
            services.AddCors(opt => opt.AddPolicy("CorsPolicy", c =>
            {
                c.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            }));

            services.AddDbContext<PatientDataContext>(options =>
                    options.UseSqlite(config.GetConnectionString("PatientDataContext")));

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Planet DDS Patient Portal",
                    Description = ".NET Core Web API for managing patient data",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Example Contact",
                        Url = new Uri("https://example.com/contact")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Example License",
                        Url = new Uri("https://example.com/license")
                    }
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

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
            services.AddHttpContextAccessor();
            services.AddHttpClient();

            services.AddScoped<IAuthenticationService<User, int>, PDDSAuthenticationService>();
            services.AddScoped<IPatientRepository<Patient, int>, PatientRepository>();
            services.AddScoped<IPatientService<Patient, int>, PatientService>();
            services.AddScoped<IUserRepository<User, int>, UserRepository>();
            services.AddScoped<IUserService<User, int>, UserService>();
            
            services.AddSingleton<IUriService>(o =>
            {
                var accessor = o.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(uri);
            });
            services.AddTransient<GlobalExceptionMiddleware>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty;
                });
            }
            else
            {
                app.UseExceptionHandler("/error"); // Replace "/error" with your custom error handling endpoint if needed
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseMiddleware<JwtMiddleware>();
            app.UseMiddleware<GlobalExceptionMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            DatabaseHelper.InitializeDatabase();
        }
    }
}
