using tupenca_back.DataAccess;
using Microsoft.EntityFrameworkCore;
using tupenca_back.DataAccess.Repository.IRepository;
using tupenca_back.DataAccess.Repository;
using tupenca_back.Services;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using tupenca_back.Utilities.EmailService;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:3000", "https://gentle-pond-0db98a110.2.azurestaticapps.net").AllowAnyMethod().AllowAnyHeader();
                      });
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection"), sqlServerOptionsAction: SqlOperations =>
    {
        SqlOperations.EnableRetryOnFailure();
    }
    ));

var emailConfig = builder.Configuration
        .GetSection("EmailConfiguration")
        .Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);
builder.Services.AddScoped<IEmailSender, EmailSender>();


// Repository
builder.Services.AddScoped<ICampeonatoRepository, CampeonatoRepository>();
builder.Services.AddScoped<IDeporteRepository, DeporteRepository>();
builder.Services.AddScoped<IEmpresaRepository, EmpresaRepository>();
builder.Services.AddScoped<IEquipoRepository, EquipoRepository>();
builder.Services.AddScoped<IEventoRepository, EventoRepository>();
builder.Services.AddScoped<IPencaCompartidaRepository, PencaCompartidaRepository>();
builder.Services.AddScoped<IPencaEmpresaRepository, PencaEmpresaRepository>();
builder.Services.AddScoped<IPersonaRepository, PersonaRepository>();
builder.Services.AddScoped<IPlanRepository, PlanRepository>();
builder.Services.AddScoped<IPrediccionRepository, PrediccionRepository>();
builder.Services.AddScoped<IPremioRepository, PremioRepository>();
builder.Services.AddScoped<IResultadoRepository, ResultadoRepository>();
builder.Services.AddScoped<IUsuarioPencaRepository, UsuarioPencaRepository>();


// Service
builder.Services.AddScoped<AdministradorService, AdministradorService>();
builder.Services.AddScoped<CampeonatoService, CampeonatoService>();
builder.Services.AddScoped<DeporteService, DeporteService>();
builder.Services.AddScoped<EmpresaService, EmpresaService>();
builder.Services.AddScoped<EquipoService, EquipoService>();
builder.Services.AddScoped<EventoService, EventoService>();
builder.Services.AddScoped<FuncionarioService, FuncionarioService>();
builder.Services.AddScoped<ImagesService, ImagesService>();
builder.Services.AddScoped<PencaService, PencaService>();
builder.Services.AddScoped<PlanService, PlanService>();
builder.Services.AddScoped<PrediccionService, PrediccionService>();
builder.Services.AddScoped<PremioService, PremioService>();
builder.Services.AddScoped<ResultadoService, ResultadoService>();
builder.Services.AddScoped<UsuarioService, UsuarioService>();


// Mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();


//Image create directory
var commonpath = Path.Combine(app.Environment.ContentRootPath, "Images");
if (!System.IO.Directory.Exists(commonpath))
{
    System.IO.Directory.CreateDirectory(commonpath);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
    app.UseExceptionHandler("/error-development");
}
else
{
    //app.UseExceptionHandler("/error");
    app.UseExceptionHandler("/error-development");
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
