using FileSanitizer.BL;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
// Add services to the container.
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ISanitizedFileSreamProvider,SanitizedFileSreamProvider>();
builder.Services.AddScoped<ISanitizedFileContentProvider,SanitizedFileContentProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();