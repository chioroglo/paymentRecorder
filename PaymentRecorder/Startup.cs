using Common.MappingProfiles;
using Data;
using Domain;
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


        services.Configure<JWTConfiguration>(Configuration.GetSection("JWT"));

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.InitializeServices();

        services.AddDbContext<EfDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        }, ServiceLifetime.Transient);


        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<EfDbContext>();

        
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

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}