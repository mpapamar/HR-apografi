using HR.Apografi.Helpers;
using HR.Apografi.Middlewares;
using HR.Apografi.Services;
using HR.Apografi.Workers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<ExceptionMiddleware>();
builder.Services.AddControllersWithViews();
builder.Services.AddMemoryCache();
builder.Services.AddTransient<OrganizationIdConverter>();
builder.Services.AddHostedService<OrganizationsBackgroundWorker>();
builder.Services.AddHostedService<DictionariesBackgroundWorker>();
builder.Services.AddHttpClient<ApografiService>(client =>
{
    client.BaseAddress = new Uri("https://hr.apografi.gov.gr/api/");
});

builder.Services.AddHttpClient<DiavgeiaService>(client =>
{
    client.BaseAddress = new Uri("https://diavgeia.gov.gr/opendata/");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
