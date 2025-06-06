var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.MapFallbackToFile("dist/client-app/browser/index.html");

//app.UseSpa(spa =>
//{
//    spa.Options.SourcePath = "ClientApp";

//    if (app.Environment.IsDevelopment())
//    {
//        spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
//    }
//});


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Employee}/{action=List}/{id?}");

app.Run();
