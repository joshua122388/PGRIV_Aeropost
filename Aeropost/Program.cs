var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Congigurar servicios de sesion
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    //tiempo de expiraci�n de la sesi�n
    options.IdleTimeout = TimeSpan.FromMinutes(20); // Tiempo de expiraci�n de la sesi�n
    options.Cookie.HttpOnly = true; // La cookie de sesi�n no es accesible desde JavaScript
    options.Cookie.IsEssential = true; // La cookie es esencial para el funcionamiento del sitio
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

//Usar la session
app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuario}/{action=Login}/{id?}");

app.Run();
