using Contracts.Services;
using Forum_Blazor.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using RESTClient.clients;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

//serialization services:
builder.Services.AddScoped<IPostService, PostClient>();
builder.Services.AddScoped<ICommentService, CommentClient>();
builder.Services.AddScoped<IUserService, UserClient>();


//log in/out services:
builder.Services.AddScoped<AuthenticationStateProvider, SimpleAuthenticationStateProvider>();
builder.Services.AddScoped<IAuthService, AuthServiceImpl>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();