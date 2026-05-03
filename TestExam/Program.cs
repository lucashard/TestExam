using TestExam;
using TestExam.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("La cadena de conexión 'DefaultConnection' no está configurada.");

builder.Services.AddScoped<IMateriaPrimaRepository>(_ =>
    new MateriaPrimaRepository(connectionString));
builder.Services.AddScoped<IDireccionRepository>(_ => new DireccionRepository(connectionString));
builder.Services.AddScoped<IClienteRepository>(_ => new ClienteRepository(connectionString));
builder.Services.AddScoped<IEtapasRepository>(_ => new EtapasRepository(connectionString));
builder.Services.AddScoped<IOperariosRepository>(_ => new OperariosRepository(connectionString));
builder.Services.AddScoped<IOperarioEtapaRepository>(_ => new OperarioEtapaRepository(connectionString));
builder.Services.AddScoped<IFacturaRepository>(_ => new FacturaRepository(connectionString));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateMateriaPrimaHandler>());

builder.Services.AddScoped<MateriaPrimaService>();
builder.Services.AddScoped<DireccionService>();
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<EtapasService>();
builder.Services.AddScoped<OperariosService>();
builder.Services.AddScoped<OperarioEtapaService>();
builder.Services.AddScoped<FacturaService>();

var app = builder.Build();
app.MapGet("/", () => Results.Ok("TestExam API en ejecucion"));

app.UseMiddleware<ApiExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthorization();

app.MapControllers();

app.Run();
