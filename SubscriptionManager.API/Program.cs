// API/Program.cs
using Microsoft.Extensions.Options;
using subscription_Domain.Repositories;
using SubscriptionManager.Infrastructure;
using SubscriptionManager.Infrastructure.Data;
using SubscriptionManager.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// Configuration MongoDB
builder.Services.Configure<MongoSettings>(
    builder.Configuration.GetSection("MongoSettings"));

// Enregistrement des services MongoDB
builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Autres configurations API...
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuration du pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();