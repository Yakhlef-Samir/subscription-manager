// API/Program.cs

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using subscription_Application.Services;
using subscription_Domain.Repositories;
using SubscriptionManager.Infrastructure;
using SubscriptionManager.Infrastructure.Data;
using SubscriptionManager.Infrastructure.Repository;
using SubscriptionManager.Infrastructure.Services;
using subscription_Application.Interfaces.Services;
using subscription_Application.Services;
using subscription_Domain.Entities;

// Au début du Program.cs, après les using
BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

var builder = WebApplication.CreateBuilder(args);

// Configuration MongoDB
builder.Services.Configure<MongoSettings>(
    builder.Configuration.GetSection("MongoSettings"));

// Enregistrement des services MongoDB
builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<DataSeeder>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();

// Autres configurations API...
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurer uniquement HTTP pour éviter les erreurs de certificat HTTPS
builder.WebHost.UseUrls("http://localhost:5171");

var app = builder.Build();

// Configuration du pipeline HTTP
if (app.Environment.IsDevelopment())
{
    /* Commenté pour éviter l'erreur de connexion MongoDB
    using (var scope = app.Services.CreateScope())
    {
        var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
        seeder.SeederUsers().Wait();
    }
    */
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Désactiver la redirection HTTPS pour éviter les erreurs de certificat
// app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

// Ajouter une route par défaut pour rediriger vers Swagger
app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();