using eduMATE_back.Services;
using Microsoft.EntityFrameworkCore;
using eduMATE_back.Data; // (si usas un DbContext que ahora armamos)


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);

// Configuración JWT
builder.Configuration["Jwt:Key"] = "2b723fa4fef84832a8e5de3481a1cfb2"; //password hardcodeado, cambiar luego en appsetting.{enviorment}.json

// Add services
builder.Services.AddSingleton<AuthService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); // <-- Para swagger
builder.Services.AddSwaggerGen();           // <-- Para swagger

var app = builder.Build();

// Configuración Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
