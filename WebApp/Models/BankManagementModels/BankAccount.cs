namespace WebApp.Models.BankManagementModels;
using System.Collections.Generic;

public class BankAccount
{
    public Guid Id { get; set; }

    public string? AccountHolder {  get; set; }

    public decimal Balance { get; set; }

    public List<Transaction> Transaction { get; set; } = new List<Transaction>();

    public BankAccount(string accountHolder)
    {
        Id = Guid.NewGuid();
        AccountHolder = accountHolder;
        Balance = 0;
    }


}
