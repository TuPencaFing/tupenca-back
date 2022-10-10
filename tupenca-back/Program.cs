using tupenca_back.DataAccess;
using Microsoft.EntityFrameworkCore;
using tupenca_back.DataAccess.Repository.IRepository;
using tupenca_back.DataAccess.Repository;
using tupenca_back.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection"), sqlServerOptionsAction: SqlOperations =>
    {
        SqlOperations.EnableRetryOnFailure();
    }
    ));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICampeonatoRepository, CampeonatoRepository>();
builder.Services.AddScoped<IEventoRepository, EventoRepository>();
builder.Services.AddScoped<IEquipoRepository, EquipoRepository>();

builder.Services.AddScoped<UserService, UserService>();
builder.Services.AddScoped<CampeonatoService, CampeonatoService>();
builder.Services.AddScoped<EventoService, EventoService>();
builder.Services.AddScoped<EquipoService, EquipoService>();
builder.Services.AddScoped<Utils, Utils>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
