using System.Net.Http.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _1_test.Data;
using _1_test.Infrastructure;
using _1_test.Models;
using _1_test.Models.Dto;
using _1_test.Models.ViewModels;

namespace _1_test.Controllers;

[Authorize]
public class PurchasesController : Controller
{
    private const string CartSessionKey = "CartItems";
    private readonly AppDbContext _context;
    private readonly IHttpClientFactory _httpClientFactory;

    public PurchasesController(AppDbContext context, IHttpClientFactory httpClientFactory)
    {
        _context = context;
        _httpClientFactory = httpClientFactory;
    }

    public IActionResult Cart()
    {
        var cart = GetCart();
        return View(cart);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddToCart(int bookId)
    {
        var book = await _context.Books.FindAsync(bookId);
        if (book == null)
        {
            return NotFound();
        }

        var cart = GetCart();
        var existing = cart.FirstOrDefault(i => i.BookId == bookId);
        if (existing == null)
        {
            cart.Add(new CartItem
            {
                BookId = book.Id,
                Title = book.Title,
                UnitPrice = book.Price,
                Quantity = 1
            });
        }
        else
        {
            existing.Quantity += 1;
        }

        SaveCart(cart);
        return RedirectToAction(nameof(Cart));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult RemoveFromCart(int bookId)
    {
        var cart = GetCart();
        var existing = cart.FirstOrDefault(i => i.BookId == bookId);
        if (existing != null)
        {
            cart.Remove(existing);
            SaveCart(cart);
        }

        return RedirectToAction(nameof(Cart));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateQuantity(int bookId, int quantity)
    {
        var cart = GetCart();
        var existing = cart.FirstOrDefault(i => i.BookId == bookId);
        if (existing != null)
        {
            existing.Quantity = Math.Max(1, quantity);
            SaveCart(cart);
        }

        return RedirectToAction(nameof(Cart));
    }

    public IActionResult Checkout()
    {
        var cart = GetCart();
        if (!cart.Any())
        {
            return RedirectToAction(nameof(Cart));
        }
        ViewBag.CartItems = cart;
        ViewBag.Total = cart.Sum(i => i.LineTotal);
        return View(new PurchaseCheckoutViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Checkout(PurchaseCheckoutViewModel model)
    {
        var cart = GetCart();
        if (!cart.Any())
        {
            ModelState.AddModelError(string.Empty, "Kosarica je prazna.");
            return View(model);
        }

        if (!ModelState.IsValid)
        {
            ViewBag.CartItems = cart;
            ViewBag.Total = cart.Sum(i => i.LineTotal);
            return View(model);
        }

        var dto = new PurchaseCreateDto
        {
            DeliveryAddress = model.DeliveryAddress,
            ContactPhone = model.ContactPhone,
            Note = model.Note,
            Items = cart.Select(item => new PurchaseItemDto
            {
                BookId = item.BookId,
                Quantity = item.Quantity
            }).ToList()
        };

        var client = _httpClientFactory.CreateClient();
        var apiUrl = $"{Request.Scheme}://{Request.Host}/api/purchases";
        var cookieHeader = Request.Headers["Cookie"].ToString();
        if (!string.IsNullOrWhiteSpace(cookieHeader))
        {
            client.DefaultRequestHeaders.Add("Cookie", cookieHeader);
        }

        var response = await client.PostAsJsonAsync(apiUrl, dto);
        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError(string.Empty, "Nakup ni uspel. Preveri podatke in zalogo.");
            ViewBag.CartItems = cart;
            ViewBag.Total = cart.Sum(i => i.LineTotal);
            return View(model);
        }

        HttpContext.Session.Remove(CartSessionKey);
        return RedirectToAction(nameof(History));
    }

    public async Task<IActionResult> History()
    {
        var client = _httpClientFactory.CreateClient();
        var apiUrl = $"{Request.Scheme}://{Request.Host}/api/purchases/my";
        var cookieHeader = Request.Headers["Cookie"].ToString();
        if (!string.IsNullOrWhiteSpace(cookieHeader))
        {
            client.DefaultRequestHeaders.Add("Cookie", cookieHeader);
        }

        var purchases = await client.GetFromJsonAsync<List<PurchaseReadDto>>(apiUrl)
            ?? new List<PurchaseReadDto>();

        return View(purchases);
    }

    [Authorize(Roles = AppRoles.Admin)]
    public async Task<IActionResult> All()
    {
        var client = _httpClientFactory.CreateClient();
        var apiUrl = $"{Request.Scheme}://{Request.Host}/api/purchases";
        var cookieHeader = Request.Headers["Cookie"].ToString();
        if (!string.IsNullOrWhiteSpace(cookieHeader))
        {
            client.DefaultRequestHeaders.Add("Cookie", cookieHeader);
        }

        var purchases = await client.GetFromJsonAsync<List<PurchaseReadDto>>(apiUrl)
            ?? new List<PurchaseReadDto>();

        return View("History", purchases);
    }

    private List<CartItem> GetCart()
    {
        return HttpContext.Session.GetObject<List<CartItem>>(CartSessionKey) ?? new List<CartItem>();
    }

    private void SaveCart(List<CartItem> cart)
    {
        HttpContext.Session.SetObject(CartSessionKey, cart);
    }
}
