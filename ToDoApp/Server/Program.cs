using Microsoft.AspNetCore.ResponseCompression;
using ToDoApp.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ToDoApp.Server.Data;
using ToDoApp.Server.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.Services.AddResponseCompression(options =>
     options.MimeTypes = ResponseCompressionDefaults
     .MimeTypes
     .Concat(new[] { "application/octet-stream" })
);


builder.Services.AddDbContext<ToDoAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ToDoAppServerContext") ?? throw new InvalidOperationException("Connection string 'ToDoAppServerContext' not found.")));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
};

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseResponseCompression();
app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapHub<ChatHub>("/chathub");
app.MapHub<AppointmentHub>("/appointmenthub");
app.MapFallbackToFile("index.html");

app.MapUserEndpoints();

app.MapAppointmentDatumEndpoints();

app.MapBoardEndpoints();

app.MapConnectionEndpoints();


app.Run();
