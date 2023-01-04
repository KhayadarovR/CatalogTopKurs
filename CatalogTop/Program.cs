using CatalogTop.DAL;
using CatalogTop.Helpers;
using CatalogTop.Models;
using CatalogTop.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<CatalogDbContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("DBcontext")));

// ����� �������������� - � ������� cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).
    AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, (conf)=> conf.LoginPath = "/Account/Login"); 
builder.Services.AddAuthorization();

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IAccountService ,AccountService>(); // ������ ��� ���������� �������� �������� (����������)

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

//TODO: �������� �����������, ���������� Id ����������� � claims
//TODO: ����� �� ���
//TODO: 

//ASK: ����������� ������ ����������? ��������� ���? ��� ���������?
//ASK: ����� �� � ������� �������� IUserRepository

//Scaffold-DbContext "Host=localhost;Database=test;Username=postgres;Password=pas" Npgsql.EntityFrameworkCore.PostgreSQL -OutputDir Models2