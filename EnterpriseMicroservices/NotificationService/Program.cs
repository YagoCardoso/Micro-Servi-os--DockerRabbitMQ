var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<INotificationService, NotificationService.Services.NotificationService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var notificationService = app.Services.GetRequiredService<INotificationService>();
notificationService.ProcessMessages();

app.Run();
