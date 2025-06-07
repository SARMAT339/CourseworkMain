var builder = WebApplication.CreateBuilder(args);

// ���������� MVC
builder.Services.AddControllersWithViews();

// ����������� ��������
builder.Services.AddSingleton<PlayfairCipherApp.Services.PlayfairService>();
builder.Services.AddSingleton<Coursework.Services.XorService>();

// ��������� ������
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// HttpContextAccessor (���� ������������)
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
app.UseSession(); // �� UseAuthorization
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();