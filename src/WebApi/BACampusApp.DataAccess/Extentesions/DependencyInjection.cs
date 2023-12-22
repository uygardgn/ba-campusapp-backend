namespace BACampusApp.DataAccess.Extentesions;
public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BACampusAppDbContext>(options =>
        {
			options.UseLazyLoadingProxies();
            options.UseSqlServer(configuration.GetConnectionString(BACampusAppDbContext.ConnectionName));
        });
        //Identity kütphanesinin sisteme entegre edilmesi.
        services.AddIdentity<IdentityUser, IdentityRole>(options =>
        {
            //Identity için kullanılacak özelliklerin kurallarının belirlenmesi.
            //options.Password.RequiredLength = 8;
            //options.Password.RequireDigit = true;
            //options.Password.RequireLowercase = true;
            //options.Password.RequireUppercase = true;
            //options.Password.RequireNonAlphanumeric = true;

            //options.SignIn.RequireConfirmedEmail = true;

            //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
            //options.Lockout.MaxFailedAccessAttempts = 5;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;

        }).AddEntityFrameworkStores<BACampusAppDbContext>().AddDefaultTokenProviders();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters()
               {
                   ValidIssuer = configuration["Jwt:Issuer"],
                   ValidAudience = configuration["Jwt:Audience"],
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"])),
                   ValidateIssuer = false,
                   ValidateAudience = false,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ClockSkew = TimeSpan.Zero
               };
           });

        services.Configure<JwtOptions>(configuration.GetSection("Jwt"));
        services.Configure<EmailOptions>(configuration.GetSection("EmailSettings"));

        return services;
    }
}
