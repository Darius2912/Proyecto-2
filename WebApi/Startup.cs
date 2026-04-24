using AppCore;

var builder = WebApplication.CreateBuilder(args);

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

/*
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5265);
    options.ListenAnyIP(7106, listenOptions =>
    {
        listenOptions.UseHttps();
    });
});
*/

var app = builder.Build();

// 🔥 MIDDLEWARE
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

app.Run();