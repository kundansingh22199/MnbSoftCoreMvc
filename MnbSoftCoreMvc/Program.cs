using BusinessAccessLayer;
using Connection;
using DataAccessLayer;
using IContract;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<ClsConnection>();
builder.Services.AddTransient<IDataAccess, DataAccess>();
builder.Services.AddScoped<IBusinessAccess, BusinessAccess>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
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
//app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        // Disable caching for profile images
        if (ctx.File.Name.EndsWith(".jpg") || ctx.File.Name.EndsWith(".png"))
        {
            ctx.Context.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            ctx.Context.Response.Headers["Pragma"] = "no-cache";
            ctx.Context.Response.Headers["Expires"] = "0";
        }
    }
    //OnPrepareResponse = ctx =>
    //{
    //    ctx.Context.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
    //    ctx.Context.Response.Headers["Pragma"] = "no-cache";
    //    ctx.Context.Response.Headers["Expires"] = "0";
    //}
});
app.UseSession();
//app.UseMiddleware<SessionCheckMiddleware>();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
