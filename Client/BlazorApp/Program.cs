using BlazorApp.Auth;
using BlazorApp.Components;
using BlazorApp.Services.CommentService;
using BlazorApp.Services.PostService;
using BlazorApp.Services.UserService;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
    

        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddScoped(sp => new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5059")
        });

        builder.Services.AddScoped<ICommentService, HttpCommentService>();
        builder.Services.AddScoped<IPostService, HttpPostService>();
        builder.Services.AddScoped<IUserService, HttpUserService>();
        builder.Services.AddScoped<AuthenticationStateProvider, SimpleAuthProvider>();

        var app = builder.Build();
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");

            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseAntiforgery();
        app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
        app.Run();
    }
}