// API/Program.cs

using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using subscription_Domain.Repositories;
using SubscriptionManager.Infrastructure;
using SubscriptionManager.Infrastructure.Data;
using SubscriptionManager.Infrastructure.Repository;
using SubscriptionManager.Infrastructure.Services;

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
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

// Autres configurations API...
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuration du pipeline HTTP
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
        await seeder.SeederUsers();
    }
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();