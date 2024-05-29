using AspNetCoreWebApi.handlers;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddAuthentication("BasicAuthentication").AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication",null);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(gen => 
    { gen.SwaggerDoc("v1.0", new Microsoft.OpenApi.Models.OpenApiInfo 
        { Title = "GarthDevice API", Version = "v1.0" }); });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(ui => 
        { ui.SwaggerEndpoint("/swagger/v1.0/swagger.json", "GarthDevice API EndPoint"); });
}
 
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
