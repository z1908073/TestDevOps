using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using TestDevOpsURL.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddAuthentication(authOptions =>
{
    authOptions.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    authOptions.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    authOptions.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    authOptions.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
}).AddOpenIdConnect(oidcOptions =>
{
    oidcOptions.ClientId = builder.Configuration["OktaWebOIDC:ClientID"];
    oidcOptions.ClientSecret = builder.Configuration["OktaWebOIDC:ClientSecret"];
    oidcOptions.Authority = builder.Configuration["OktaWebOIDC:Issuer"];
    oidcOptions.CallbackPath = "/authorization-code/callback";
    oidcOptions.ResponseType = "code";
    oidcOptions.SaveTokens = true;
    oidcOptions.Scope.Add("openid");
    oidcOptions.Scope.Add("profile");
    oidcOptions.TokenValidationParameters.ValidateIssuer = false;
    oidcOptions.TokenValidationParameters.NameClaimType = "name";
}).AddCookie();



var app = builder.Build();

app.Use(async (context, next) =>
{
    context.Request.Scheme = "https";

   
    await next();

});

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

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapBlazorHub();
    endpoints.MapFallbackToPage("/_Host");
});

app.Run();
