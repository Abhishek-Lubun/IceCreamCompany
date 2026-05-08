using InsecureCompany.Interfaces;
using InsecureCompany.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IDataLoaderService,
                           DataLoaderService>();

builder.Services.AddScoped<IReportService,
                           ReportService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern:
    "{controller=Report}/{action=Index}/{id?}");

app.Run();