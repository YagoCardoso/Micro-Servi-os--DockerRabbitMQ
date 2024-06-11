// Add services to the container
// Configurar EF Core com SQL Server
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
builder.Configuration["ConnectionStrings"];

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(connectionString));

// configurando injecao de dependencia
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
app.UseSwagger();
app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();