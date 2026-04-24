using AppCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// 🔥 ESTO FALTABA
builder.WebHost.UseWebRoot("wwwroot");

builder.Services.AddScoped<PropiedadManager>();
builder.Services.AddScoped<EvaluacionManager>();
builder.Services.AddTransient<AppCore.CorreoManager>();
builder.Services.AddTransient<AppCore.UsuarioManager>();
builder.Services.AddScoped<CorreoManager>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseStaticFiles(); // ✔ correcto
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "wwwroot")),
    RequestPath = ""
});
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers();

app.Run();