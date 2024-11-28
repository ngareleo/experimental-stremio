using Microsoft.EntityFrameworkCore;
using Stremio.Models;
using Stremio.Providers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<VideoContext>(opt => opt.UseInMemoryDatabase("Videos"));
builder.Services.AddSingleton<DiskProvider>();
builder.Services.AddScoped<FFmpegProvider>();
builder.Services.AddSingleton<MediaMetadataProvider>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scopedServices = app.Services.CreateScope())
{
    var services = scopedServices.ServiceProvider;
    var diskProvider = services.GetRequiredService<DiskProvider>();
    diskProvider.OnStartup();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
