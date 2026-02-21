using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecureVault.Application.DTOs;
using SecureVault.Application.Interfaces;
using System.Security.Claims;

namespace SecureVault.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaultController : ControllerBase
    {
        private readonly IVaultService _vaultService;

        public VaultController(IVaultService vaultService)
        {
            _vaultService = vaultService;
        }

        private int GetUserId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null)
            {
                throw new UnauthorizedAccessException();
            }
            return int.Parse(claim.Value);

        }

        // ADD SECRET
        [HttpPost]
        public async Task<IActionResult> Add(CreateVaultItemDTO dto)
        {
            try
            {
                if (dto == null || string.IsNullOrWhiteSpace(dto.Title) || string.IsNullOrWhiteSpace(dto.Data))
                {
                    return BadRequest(new { message = "Invalid data" });
                }


                await _vaultService.AddItemAsync(GetUserId(), dto);
                return Ok(new { message = "Saved successfully" });


            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { message = "Invalid token" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Server error",
                    error = ex.Message
                });
            }
        }

        // GET MY VAULT ITEMS
        [Authorize]
        [HttpGet("my")]
        public async Task<IActionResult> GetMyItems()
        {
            try
            {
                var userId = GetUserId();

                var items = await _vaultService.GetMyItemsAsync(userId);

                return Ok(new
                {
                    success = true,
                    data = items
                });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { message = "Invalid token" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Server error",
                    error = ex.Message
                });
            }
        }


        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateVaultItemDTO dto)
        {
            try
            {
                var userId = GetUserId();

                await _vaultService.UpdateAsync(id, userId, dto);

                return Ok(new
                {
                    success = true,
                    message = "Vault item updated"
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Server error",
                    error = ex.Message
                });
            }
        }



        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var userId = GetUserId();

                await _vaultService.DeleteAsync(userId, id);

                return Ok(new
                {
                    success = true,
                    message = "Item deleted successfully"
                });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { message = "Item not found or access denied" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Server error",
                    error = ex.Message
                });
            }
        }


        [Authorize]
        [HttpGet("search")]
        public async Task<IActionResult> Search(string? text, string? category)
        {
            try
            {
                var userId = GetUserId();
                var result = await _vaultService.SearchAsync(userId, text, category);

                return Ok(new
                {
                    success = true,
                    data = result
                });
            }
            catch
            {
                return StatusCode(500, new { message = "Server error" });
            }
        }

    }
}