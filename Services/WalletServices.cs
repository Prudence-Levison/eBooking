using System.Transactions;
using eBooking.Data;
using eBooking.Domain;
using eBooking.Enums;
using eBooking.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

public  class WalletServices : IWalletServices
{
    private readonly ApplicationDbContext _context;
    public WalletServices(ApplicationDbContext context)
    {
        _context = context;
    }

    // get wallet by user id

    public async Task<Wallet> GetWalletByUserIdAsync(Guid userId)
    {
         var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserId == userId);
         if(wallet == null)
         {
         throw new KeyNotFoundException($"Wallet for user with ID {userId} not found.");
          }
         return wallet;
    }

    public async Task<Wallet>EnsureWalletExistsAsync(Guid UserId)
    {
        var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserId == UserId);
        if (wallet == null)
        {
           wallet = new Wallet
           {
               UserId = UserId,
               Balance = 0
           };
           _context.Wallets.Add(wallet);
           await _context.SaveChangesAsync();
        }
        return wallet;
    }
    public async Task<Wallet>TopUpWalletAsync(Guid UserId, decimal amount)
    {
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Amount must be greater than zero.");
            }
        
            var wallet = await EnsureWalletExistsAsync(UserId);
            wallet.Balance += amount;
            var transactionHistory = new TransactionHistory
            {
                UserId = UserId,
                WalletId = wallet.Id,
                Amount = amount,
                Status = PaymentStatus.Successful,
                PaymentMethod = "Card", // Assuming top-up is done via card, this can be dynamic based on actual payment method used.
                TransactionType = "Credit",
                Description = $"Wallet top-up of {amount} naira",
                TransactionDate = DateTime.UtcNow,
                ReferenceNumber = Guid.NewGuid().ToString() // Generate a unique reference number for the transaction.
            };
            _context.TransactionHistories.Add(transactionHistory);
            _context.Wallets.Update(wallet);
            await _context.SaveChangesAsync();
            return wallet;   
        }}
        public async Task<Wallet> DebitWalletAsync(Guid UserId, decimal amount)
    {
        if (amount <=0)
        {
            throw new Exception ("Amount must be greater than Zero.");
        }
        var wallet = await EnsureWalletExistsAsync(UserId);
        if (wallet.Balance < amount)
        {
            throw new Exception("Insufficient Funds");
        }
        wallet.Balance -= amount;
        var transactionHistory = new TransactionHistory
        {
            UserId = UserId,
            WalletId = wallet.Id,
            Amount = amount,
            Status = PaymentStatus.Successful,
            PaymentMethod = "Wallet",
            TransactionType = "Debit",
            Description = $"Wallet debit of {amount} naira",
            TransactionDate = DateTime.UtcNow,
            ReferenceNumber = Guid.NewGuid().ToString() // Generate a unique reference number for the transaction.
        };
        _context.TransactionHistories.Add(transactionHistory);
        _context.Wallets.Update(wallet);
        await _context.SaveChangesAsync();
        return wallet;
    }
    }
