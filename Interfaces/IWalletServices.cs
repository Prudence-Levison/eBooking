namespace eBooking.Interfaces
{
    public interface IWalletServices
    {
        Task<Wallet> GetWalletByUserIdAsync(Guid userId);
        Task<Wallet> EnsureWalletExistsAsync(Guid userId);
        Task <Wallet>TopUpWalletAsync(Guid UserId, decimal  amount);
        Task<Wallet> DebitWalletAsync(Guid UserId, decimal amount);
        // Task<Wallet> UpdateWalletBalanceAsync(Guid userId, decimal amount);
    }
}