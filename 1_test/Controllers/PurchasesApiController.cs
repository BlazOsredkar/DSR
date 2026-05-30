using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _1_test.Data;
using _1_test.Infrastructure;
using _1_test.Models;
using _1_test.Models.Dto;

namespace _1_test.Controllers;

[Authorize]
[ApiController]
[Route("api/purchases")]
public class PurchasesApiController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public PurchasesApiController(AppDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpGet("my")]
    public async Task<IActionResult> GetMyPurchases()
    {
        var userId = _userManager.GetUserId(User);
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Unauthorized();
        }

        var purchases = await _context.Purchases
            .Include(p => p.Items)
            .ThenInclude(i => i.Book)
            .Where(p => p.UserId == userId)
            .OrderByDescending(p => p.PurchaseDate)
            .ToListAsync();

        return Ok(purchases.Select(MapToReadDto));
    }

    [Authorize(Roles = AppRoles.Admin)]
    [HttpGet]
    public async Task<IActionResult> GetAllPurchases()
    {
        var purchases = await _context.Purchases
            .Include(p => p.Items)
            .ThenInclude(i => i.Book)
            .OrderByDescending(p => p.PurchaseDate)
            .ToListAsync();

        return Ok(purchases.Select(MapToReadDto));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPurchase(int id)
    {
        var userId = _userManager.GetUserId(User);
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Unauthorized();
        }

        var purchase = await _context.Purchases
            .Include(p => p.Items)
            .ThenInclude(i => i.Book)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (purchase == null)
        {
            return NotFound();
        }

        if (!User.IsInRole(AppRoles.Admin) && purchase.UserId != userId)
        {
            return Forbid();
        }

        return Ok(MapToReadDto(purchase));
    }

    [HttpPost]
    public async Task<IActionResult> CreatePurchase([FromBody] PurchaseCreateDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userId = _userManager.GetUserId(User);
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Unauthorized();
        }

        var bookIds = dto.Items.Select(i => i.BookId).Distinct().ToList();
        var books = await _context.Books.Where(b => bookIds.Contains(b.Id)).ToListAsync();

        if (books.Count != bookIds.Count)
        {
            return BadRequest("Izbrani izdelek ne obstaja.");
        }

        var purchase = new Purchase
        {
            UserId = userId,
            DeliveryAddress = dto.DeliveryAddress,
            ContactPhone = dto.ContactPhone,
            Note = dto.Note,
            Status = "Novo",
            PurchaseDate = DateTime.UtcNow
        };

        foreach (var item in dto.Items)
        {
            var book = books.Single(b => b.Id == item.BookId);
            if (book.Stock < item.Quantity)
            {
                return BadRequest($"Ni dovolj zaloge za: {book.Title}.");
            }

            book.Stock -= item.Quantity;
            purchase.Items.Add(new PurchaseItem
            {
                BookId = book.Id,
                Quantity = item.Quantity,
                UnitPrice = book.Price
            });
        }

        purchase.TotalPrice = purchase.Items.Sum(i => i.UnitPrice * i.Quantity);

        _context.Purchases.Add(purchase);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPurchase), new { id = purchase.Id }, MapToReadDto(purchase));
    }

    private static PurchaseReadDto MapToReadDto(Purchase purchase)
    {
        return new PurchaseReadDto
        {
            Id = purchase.Id,
            PurchaseDate = purchase.PurchaseDate,
            TotalPrice = purchase.TotalPrice,
            Status = purchase.Status,
            DeliveryAddress = purchase.DeliveryAddress,
            ContactPhone = purchase.ContactPhone,
            Note = purchase.Note,
            Items = purchase.Items.Select(i => new PurchaseItemReadDto
            {
                BookId = i.BookId,
                BookTitle = i.Book?.Title ?? string.Empty,
                UnitPrice = i.UnitPrice,
                Quantity = i.Quantity,
                LineTotal = i.UnitPrice * i.Quantity
            }).ToList()
        };
    }
}
