using Common.Jwt;
using Common.MappingProfiles;
using Data;
using Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PaymentRecorder.Extensions;

namespace PaymentRecorder;

public class Startup
{

    public IConfiguration Configuration { get; set; }
    
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        string connectionString = Configuration.GetConnectionString("DefaultConnection");
        

        
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.InitializeServices();

        services.Configure<JWTConfigurationFromAppsettingsJson>(Configuration.GetSection("JWT"));
        services.ConfigureIdentity();
        services.ConfigureFluentValidation();


        services.AddDbContext<EfDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        }, ServiceLifetime.Transient);


        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<EfDbContext>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).ConfigureJwtBearer(Configuration);
        
        
        services.AddAutoMapper(typeof(MappingAssemblyMarker).Assembly);

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCustomExceptionHandling();
        app.UseDbTransactionPerRequest();
        app.UseVersionHeaderChecker();

        app.UseAuthentication();
        app.UseAuthorization();


        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}