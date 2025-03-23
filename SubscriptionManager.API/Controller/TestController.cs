using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using SubscriptionManager.Infrastructure.Data;

namespace SubscriptionManager.API.Controller;

[ApiController]
[Route("api/test")]
public class TestController : ControllerBase
{
    private readonly MongoDbContext _context;

    public TestController(MongoDbContext context)
    {
        _context = context;
    }

    [HttpGet("ping")]
    public async Task<IActionResult> PingDatabase()
    {
        try
        {
            await _context.Database.RunCommandAsync(
                (Command<BsonDocument>)"{ping:1}");
            return Ok("Database connection successful!");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Database connection failed: {ex.Message}");
        }
    }
}