public class Wallet
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public decimal Balance { get; set; }

    public decimal AmountCredited {get; set;}

    public decimal AmountDebited {get; set;}
    public  decimal TotalAmount {get; set;}

    public string? TransactionHistory {get; set;}
    public string? Description {get; set;}
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

}