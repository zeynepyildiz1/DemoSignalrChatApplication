using DemoSignalrChatServerApplication.Hubs;

var builder = WebApplication.CreateBuilder(args);

// CORS ayarlarını ekle
builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy
    .AllowCredentials()
    .AllowAnyHeader()
    .AllowAnyMethod()
    .SetIsOriginAllowed(x => true)));

// SignalR'ı ekle
builder.Services.AddSignalR();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// CORS middleware'ı ekle
app.UseCors();

app.MapGet("/", () => "Hello World!");

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ChatHub>("/chathub");
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("Hello World!");
    });
});

app.Run();