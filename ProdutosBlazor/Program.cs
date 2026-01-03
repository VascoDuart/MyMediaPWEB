using Microsoft.AspNetCore.Components.Authorization;
using ProdutosBlazor.Components;
using RCLAPI.Services;
using RCLComum.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped(sp => new HttpClient {
    BaseAddress = new Uri("https://localhost:7281/")
});

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<AuthService>();

builder.Services.AddScoped<IProdutoService, RCLAPI.Services.ProdutoService>();

builder.Services.AddAuthorizationCore();

var app = builder.Build();

if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(typeof(RCLProdutos.Pages.Catalogo).Assembly); 

app.Run();
