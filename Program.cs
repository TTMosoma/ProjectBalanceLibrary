using Microsoft.EntityFrameworkCore;
using ProjectBalanceLibrary.Components;
using ProjectBalanceLibrary.Contracts;
using ProjectBalanceLibrary.Data;
using ProjectBalanceLibrary.Services;
using Radzen;

namespace ProjectBalanceLibrary
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents().AddInteractiveServerComponents();

            // EF Core (SQLite)
            builder.Services.AddDbContext<AppDbContext>(opt =>
                opt.UseSqlite(builder.Configuration.GetConnectionString("Default") ?? "Data Source=projectbalance-library.db"));


            // Radzen services
            builder.Services.AddRadzenComponents();

            // Domain contracts and services
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<ILenderService, LenderService>();
            builder.Services.AddScoped<ILoanService, LoanService>();

            var app = builder.Build();

            // Ensure database exists and run migrations
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.Migrate();
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
