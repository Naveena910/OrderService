using Contracts.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository;
using Services;

using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Contracts.IService;
using Services.Client;

namespace OrderService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) // Configure services 
        {

            services.AddHttpContextAccessor();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            services.AddScoped<IServices, Service>();
            services.AddScoped<IOrderRepository,OrderRepository>();
            services.AddSwaggerGen();
            services.AddControllers();
            services.AddAutoMapper(typeof(Mappers));
            services.AddScoped<IOrderService, OrderServices>();
            services.AddHttpClient();
            services.AddScoped<ProductClient>();


            services.AddDbContext<RepositoryContext>(options => options.UseSqlServer(Configuration.GetConnectionString("OrderDb")));
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                var Key = Encoding.UTF8.GetBytes(Configuration["JWT:key"]);
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JWT:Issuer"],
                    ValidAudience = Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Key)
                };
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                /* app.UseExceptionHandler(appBuilder =>
                 {
                     appBuilder.Run(async c =>
                     {
                         c.Response.StatusCode = 500;
                         await c.Response.WriteAsync("Something went wrong, please try again later!");
                     });
                 });
             }*/

                app.UseRouting();
                app.UseAuthentication();
                app.UseAuthorization();
                app.UseSwagger();

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "APIs");
                });
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            }
        }

    }
}