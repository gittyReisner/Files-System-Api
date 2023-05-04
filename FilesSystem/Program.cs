
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => {
            builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });

});
builder.Services.AddControllers();
var app = builder.Build();

app.UseCors("AllowAllOrigins");

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();