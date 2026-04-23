using Payments.API.Data;
using Payments.API.Services;

var builder = WebApplication.CreateBuilder(args);

// --- 1. רישום שירותים (Services) לתוך ה-Container ---

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// הגדרת CORS - מאפשר ל-React לגשת לנתונים
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact",
        policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// **כאן ה-Dependency Injection**
// אנחנו אומרים למערכת: "כשמישהו מבקש ממשק, תביאי לו את המחלקה הזו"
builder.Services.AddScoped<ILogRepository, LogRepository>();
builder.Services.AddScoped<ILogService, LogService>();

// --- 2. בניית האפליקציה ---
var app = builder.Build();

// --- 3. הגדרת הצינור (Pipeline) - איך הבקשות עוברות ---

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// חשוב: UseCors חייב לבוא לפני Authorization ומפות הקונטרולרים
app.UseCors("AllowReact");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();