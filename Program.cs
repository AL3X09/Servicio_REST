using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Servicio_REST.Data;
using Servicio_REST.Middleware;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    //options.SwaggerDoc("v1", new OpenApiInfo { Title = "Servicio RestFul RIPS", Version = "v1" });
    options.SwaggerDoc("v1",
        new OpenApiInfo()
        {
            Title = "Servicio RestFull",
            Version = "0.0.1",
            Description = "Un Servicio RestFul de pruebas",
            //TermsOfService = new Uri("https://example.com/terms"),//terminos de uso
            Contact = new OpenApiContact()
            {
                //Email = "hdquintana@saludcapital.gov.co;asistenciarips@saludcapital.gov.co",
                //Name = "Equipo de Gestión de la Información",
            }
        });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    options.AddSecurityDefinition("XApiKey", new OpenApiSecurityScheme
    {
        Description = "Acceso solo con credenciales",
        Type = SecuritySchemeType.ApiKey,
        Name = "XApiKey",
        In = ParameterLocation.Header,
        //Scheme = "ApiKeyScheme"
    });
    var key = new OpenApiSecurityScheme()
    {
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "XApiKey"
        },
        In = ParameterLocation.Header
    };
    var requirement = new OpenApiSecurityRequirement
                    {
                             { key, new List<string>() }
                    };
    options.AddSecurityRequirement(requirement);
    //other configs;
    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});


#region conexión a la base de datos
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DB_Context>(options =>
    options.UseSqlServer(connectionString));
#endregion

#region configuración de los CORS
builder.Services.AddCors(p => p.AddPolicy("corspublico", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region configuración del entorno cuando la solución esta en modo depuración o producción
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        options.InjectJavascript("/swagger/lang/translator.js");
        options.InjectJavascript("/swagger/lang/custom.js");
    });

}
else
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger(c =>
    {
        c.RouteTemplate = "/swagger/{documentName}/swagger.json";
    });
    app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "v1"));
}
#endregion

//https://auth0.com/blog/force-https-in-aspnet-core-apps/
app.UseHttpsRedirection();
app.UseRouting();

//app cors
app.UseCors("corspublico");


#region Midlewaare que controla el acceso a la API
app.UseMiddleware<ApiKeyMiddleware>();
#endregion

#region Se unen para la Utorización
//app.UseAuthorization();
//app.MapControllers();
#endregion



app.Run();

