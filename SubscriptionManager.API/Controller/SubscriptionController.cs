using Microsoft.AspNetCore.Mvc;
using subscription_Domain.Enums;
using subscription_Application.Interfaces.Services;
using subscription_Application.DTOs.Subscriptions;

namespace SubscriptionManager.API.Controller;

[ApiController]
[Route("api/[controller]")]
public class SubscriptionController : ControllerBase
{
    private readonly ISubscriptionService _subscriptionService;

    public SubscriptionController(ISubscriptionService subscriptionService)
    {
        _subscriptionService = subscriptionService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SubscriptionDto>>> GetAllSubscriptions()
    {
        var subscriptions = await _subscriptionService.GetAllSubscriptionsAsync();
        return Ok(subscriptions);
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<SubscriptionDto>>> GetAllByUserId(Guid userId)
    {
        var subscriptions = await _subscriptionService.GetAllByUserIdAsync(userId);
        return Ok(subscriptions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SubscriptionDto>> GetById(Guid id)
    {
        var subscription = await _subscriptionService.GetByIdAsync(id);
        return Ok(subscription);
    }

    [HttpGet("category/{categoryId}")]
    public async Task<ActionResult<IEnumerable<SubscriptionDto>>> GetByCategory(Guid categoryId)
    {
        var subscriptions = await _subscriptionService.GetByCategoryAsync(categoryId);
        return Ok(subscriptions);
    }

    [HttpPost]
    public async Task<ActionResult<SubscriptionDto>> Create(CreateSubscriptionDto dto)
    {
        var subscription = await _subscriptionService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = subscription.Id }, subscription);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<SubscriptionDto>> Update(Guid id, UpdateSubscriptionDto dto)
    {
        var subscription = await _subscriptionService.UpdateAsync(id, dto);
        if (subscription == null) return NotFound();
        return Ok(subscription);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _subscriptionService.DeleteAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }

    [HttpGet("active")]
    public async Task<ActionResult<IEnumerable<SubscriptionDto>>> GetActiveSubscriptions()
    {
        var subscriptions = await _subscriptionService.GetActiveSubscriptionsAsync();
        return Ok(subscriptions);
    }

    [HttpGet("status/{status}")]
    public async Task<ActionResult<IEnumerable<SubscriptionDto>>> GetByStatus(SubscriptionStatus status)
    {
        var subscriptions = await _subscriptionService.GetByStatusAsync(status);
        return Ok(subscriptions);
    }

    [HttpGet("expiring/{days}")]
    public async Task<ActionResult<IEnumerable<SubscriptionDto>>> GetExpiringSoon(int days)
    {
        var subscriptions = await _subscriptionService.GetExpiringSoonAsync(days);
        return Ok(subscriptions);
    }

    [HttpGet("user/{userId}/monthly-expense")]
    public async Task<ActionResult<decimal>> GetTotalMonthlyExpense(Guid userId)
    {
        var expense = await _subscriptionService.GetTotalMonthlyExpenseAsync(userId);
        return Ok(expense);
    }

    [HttpGet("user/{userId}/expiring-this-month")]
    public async Task<ActionResult<IEnumerable<SubscriptionDto>>> GetSubscriptionsExpiringThisMonth(Guid userId)
    {
        var subscriptions = await _subscriptionService.GetSubscriptionsExpiringThisMonthAsync(userId);
        return Ok(subscriptions);
    }
}