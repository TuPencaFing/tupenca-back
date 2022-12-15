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
using tupenca_back.Model.Notification;
using Quartz;
using tupenca_back.Services.Scheduler;
using CorePush.Google;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:3000", " https://admin-tupenca-tsi.azurewebsites.net", "https://tupenca-user-front.azurewebsites.net", "https://admin-tupenca-tsi.azurewebsites.net").AllowAnyMethod().AllowAnyHeader();
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


builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionScopedJobFactory();

  
    var NotificationEventoProximoJobKey = new JobKey("NotificationEventoProximoJob");

    q.AddJob<NotificationEventoProximoJob>(opts => opts.WithIdentity(NotificationEventoProximoJobKey));


    var PuntajeJobKey = new JobKey("PuntajeJob");

    q.AddJob<PuntajeJob>(opts => opts.WithIdentity(PuntajeJobKey));


    q.AddTrigger(opts => opts
        .ForJob(NotificationEventoProximoJobKey)
        .WithIdentity("NotificationEventoProximoJob-trigger")
        .WithCronSchedule("0 0 21 ? * * *"));

    q.AddTrigger(opts => opts
        .ForJob(PuntajeJobKey)
        .WithIdentity("PuntajeJob-trigger")
        .WithCronSchedule("0 0 20 ? * * *"));


});
builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

// Repository
builder.Services.AddScoped<ICampeonatoRepository, CampeonatoRepository>();
builder.Services.AddScoped<IDeporteRepository, DeporteRepository>();
builder.Services.AddScoped<IEmpresaRepository, EmpresaRepository>();
builder.Services.AddScoped<IEquipoRepository, EquipoRepository>();
builder.Services.AddScoped<IEventoRepository, EventoRepository>();
builder.Services.AddScoped<IForoRepository, ForoRepository>();
builder.Services.AddScoped<IPencaCompartidaRepository, PencaCompartidaRepository>();
builder.Services.AddScoped<IPencaEmpresaRepository, PencaEmpresaRepository>();
builder.Services.AddScoped<IPersonaRepository, PersonaRepository>();
builder.Services.AddScoped<IPlanRepository, PlanRepository>();
builder.Services.AddScoped<IPrediccionRepository, PrediccionRepository>();
builder.Services.AddScoped<IPremioRepository, PremioRepository>();
builder.Services.AddScoped<IPuntajeRepository, PuntajeRepository>();
builder.Services.AddScoped<IPuntajeUsuarioPencaRepository, PuntajeUsuarioPencaRepository>();
builder.Services.AddScoped<IResultadoRepository, ResultadoRepository>();
builder.Services.AddScoped<IUsuarioPencaRepository, UsuarioPencaRepository>();
builder.Services.AddScoped<ILookAndFeelRepository, LookAndFeelRepository>();
builder.Services.AddScoped<IUsuarioPremioRepository, UsuarioPemioRepository>();



// Service
builder.Services.AddScoped<AdministradorService, AdministradorService>();
builder.Services.AddScoped<CampeonatoService, CampeonatoService>();
builder.Services.AddScoped<DeporteService, DeporteService>();
builder.Services.AddScoped<EmpresaService, EmpresaService>();
builder.Services.AddScoped<EquipoService, EquipoService>();
builder.Services.AddScoped<EventoService, EventoService>();
builder.Services.AddScoped<ForoService, ForoService>();
builder.Services.AddScoped<FuncionarioService, FuncionarioService>();
builder.Services.AddScoped<ImagesService, ImagesService>();
builder.Services.AddScoped<PencaService, PencaService>();
builder.Services.AddScoped<PlanService, PlanService>();
builder.Services.AddScoped<PrediccionService, PrediccionService>();
builder.Services.AddScoped<PremioService, PremioService>();
builder.Services.AddScoped<PuntajeService, PuntajeService>();
builder.Services.AddScoped<PuntajeUsuarioPencaService, PuntajeUsuarioPencaService>();
builder.Services.AddScoped<ResultadoService, ResultadoService>();
builder.Services.AddScoped<UsuarioService, UsuarioService>();
builder.Services.AddScoped<UsuarioPremioService, UsuarioPremioService>();
builder.Services.AddScoped<LookAndFeelService, LookAndFeelService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddTransient<INotificationService, NotificationService>();
builder.Services.AddHttpClient<FcmSender>();
// Configure strongly typed settings objects
var appSettingsSection = builder.Configuration.GetSection("FcmNotification");
builder.Services.Configure<FcmNotificationSetting>(appSettingsSection);

// Mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();



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
