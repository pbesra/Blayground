using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UsePathBase("/auth");
app.Use(async (context, next) =>
{
    if (!context.Request.Path.StartsWithSegments("/auth"))
    {
        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
    }
    await next.Invoke();
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();


app.UseAuthorization();

app.MapControllers();

app.Run();
