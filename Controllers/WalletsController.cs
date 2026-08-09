using eBooking.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

namespace eBooking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletsController : ControllerBase
    {
        public readonly IWalletServices _walletServices;

        public WalletsController(IWalletServices walletServices)
        {
            _walletServices = walletServices;
        }
        [HttpGet("user/{UserId}")]
        public async Task <IActionResult> GetWalletByUserId(Guid UserId)
        {
            var wallet = await _walletServices.GetWalletByUserIdAsync(UserId);
            if(wallet == null)
            {
                return NotFound();
            }
            return Ok(wallet);
        }
        [HttpPost("user/{UserId}")]
        public async Task<IActionResult> CreateWallet(Guid UserId)
        {
            var wallet = await _walletServices.EnsureWalletExistsAsync(UserId);
            return Ok(wallet);
        }
        [HttpPost("user/{UserId}/top-up")]
        public async Task<IActionResult> TopUpWallet(Guid UserId, decimal amount)
        {
            var wallet = await _walletServices.TopUpWalletAsync(UserId, amount);
            return Ok(wallet);
        }
        [HttpPost("user/{UserId}/debit")]
        public async Task<IActionResult> DerbitwWallet(Guid UserId, decimal amount)
        {
            var wallet = await  _walletServices.DebitWalletAsync(UserId, amount);
            return Ok(wallet);
        }
    }
}