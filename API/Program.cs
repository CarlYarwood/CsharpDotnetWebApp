// program.cs
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using API.Data;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Identity;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers().AddNewtonsoftJson(s => {
            s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        });


        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var sqlConBuilder = new SqlConnectionStringBuilder();

        sqlConBuilder.ConnectionString = builder.Configuration.GetConnectionString("SQLDbConnection");
        sqlConBuilder.UserID = builder.Configuration["UserId"];
        sqlConBuilder.Password = builder.Configuration["Password"];

        builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(sqlConBuilder.ConnectionString));
        builder.Services.AddAuthorization();
        builder.Services.AddIdentityApiEndpoints<IdentityUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>();
        builder.Services.AddScoped<ICareTeamRepo, CareTeamRepo>();
        builder.Services.AddScoped<IPatientRepo, PatientRepo>();
        builder.Services.AddScoped<IRecommendationRepo, RecommendationRepo>();
        builder.Services.AddScoped<IUserCareTeamRepo, UserCareTeamRepo>();
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                policy  =>
                    {
                        policy.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost").AllowAnyHeader().AllowAnyMethod();
                    }
            );
        });

        var app = builder.Build();
        
        app.UseCors();

        app.MapIdentityApi<IdentityUser>();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        using (var scope = app.Services.CreateScope())
        {
            var roleManger = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var roles = new [] { "Admin", "User" };
            foreach(var role in roles)
            {
                if(!await roleManger.RoleExistsAsync(role))
                {
                    await roleManger.CreateAsync(new IdentityRole(role));
                }
            }
        }

        using (var scope = app.Services.CreateScope())
        {
            var userManger = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            string email = "admin@admin.com";
            string password = "Test1234!";
            if (await userManger.FindByEmailAsync(email) == null)
            {
                var user = new IdentityUser();
                user.UserName = email;
                user.Email = email;

                await userManger.CreateAsync(user, password);
                await userManger.AddToRoleAsync(user, "Admin");
            }
        }

        app.Run();

    }
}
