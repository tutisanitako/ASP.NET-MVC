using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{sessionId}")]
        public async Task<IActionResult> GetCart(string sessionId)
        {
            var items = await _cartService.GetCartItemsAsync(sessionId);
            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var item = await _cartService.AddToCartAsync(dto);
            return Ok(item);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateQuantity(int id, [FromBody] int quantity)
        {
            var result = await _cartService.UpdateCartItemAsync(id, quantity);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var result = await _cartService.RemoveFromCartAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}