var builder = WebApplication.CreateBuilder(args);

// Добавление MVC
builder.Services.AddControllersWithViews();

// Регистрация сервисов
builder.Services.AddSingleton<PlayfairCipherApp.Services.PlayfairService>();
builder.Services.AddSingleton<Coursework.Services.XorService>();

// Поддержка сессий
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// HttpContextAccessor (если используется)
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession(); // до UseAuthorization
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();