using AnimalesMVC.Servicios.Configuracion;
using AnimalesMVC.Servicios.Contratos;
using AnimalesMVC.Servicios.Implementacion;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Agregamos la conexion a bbdd
builder.Services.Configure<ConfiguracionConexion>
    (builder.Configuration.GetSection("ConfiguracionConexion"));
//Agregamos servicios por tabla
builder.Services.AddScoped<IAnimalService, AnimalService>();
builder.Services.AddScoped<ITipoAnimalService, TipoAnimalService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
