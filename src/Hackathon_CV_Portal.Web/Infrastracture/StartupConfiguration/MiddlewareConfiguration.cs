using Hackathon_CV_Portal.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Hackathon_CV_Portal.Web.Infrastracture.StartupConfiguration
{
    public static class MiddlewareConfiguration
    {
        public static WebApplication ConfigureMiddleware(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseDatabaseMigration<CvPortalDbContext>();

            //using (var scope = app.Services.CreateScope())
            //{
            //    var dataContext = scope.ServiceProvider.GetRequiredService<CvPortalDbContext>();
            //    dataContext.Database.Migrate();
            //}

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            return app;
        }

        public static void UseDatabaseMigration<TDbContext>(this IApplicationBuilder builder) where TDbContext : DbContext
        {
            using (IServiceScope serviceScope = builder.ApplicationServices.CreateScope())
            {
                TDbContext service = serviceScope.ServiceProvider.GetService<TDbContext>();
                if (service != null)
                    service.Database.Migrate();
            }
        }
    }


}
