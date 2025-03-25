using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using subscription_Domain.Entities;
using SubscriptionManager.Infrastructure.Services;

namespace SubscriptionManager.API.Controller;

[ApiController]
[Route("api/[controller]")]
public class SeedController : ControllerBase
{
    private readonly DataSeeder _seeder;
    private IMongoCollection<User> _usersCollection;

    public SeedController(DataSeeder seeder)
    {
        _seeder = seeder;
    }

    [HttpPost("users")]
    public async Task<IActionResult> SeedUsers()
    {
        try
        {
            await _seeder.SeederUsers();
            return Ok("Users seeded successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost("clean")]
    public async Task<IActionResult> CleanDatabase()
    {
        await _usersCollection.DeleteManyAsync(_ => true);
        return Ok("Base de données nettoyée");
    }
}