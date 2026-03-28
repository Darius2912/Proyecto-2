var builder = WebApplication.CreateBuilder(args);

// Servicios para Razor Pages y API
builder.Services.AddRazorPages();
builder.Services.AddControllers();

// Opcional: habilitar CORS si tu WebApp está en otro puerto
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Usar CORS si lo configuraste
app.UseCors("AllowAll");

// Mapear Razor Pages y Controladores
app.MapRazorPages();
app.MapControllers();

// Redirigir raíz "/" hacia Login
app.MapGet("/", () => Results.Redirect("/Login"));

app.Run();
