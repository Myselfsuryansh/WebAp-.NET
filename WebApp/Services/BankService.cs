using WebApp.Models.BankManagementModels;

namespace WebApp.Services;
using System.Linq;
using System.Collections.Generic;
using System;

public class BankService
{
    private List<BankAccount> _accounts = new List<BankAccount>();

    public BankAccount CreateAccount(string accountHolder)
    {
        var newAccount = new BankAccount(accountHolder);
        _accounts.Add(newAccount);
        return newAccount;
    }

    public BankAccount? GetAccount(Guid id)
    {
        var account = _accounts.FirstOrDefault(x => x.Id == id);
        if(account == null)
        {
            return null;
        }
        return account;
    }
    public bool Deposit(Guid accountId, decimal amount)
    {
        var account = GetAccount(accountId);
        if (account == null  || amount <=0)
        {
            return false;


        }
        account.Balance = account.Balance + amount;
        account.Transaction.Add(new Transaction(amount, "Deposit", $"Deposit{amount:C}"));
        return true;
    }

    public bool Withdrawn(Guid accountId, decimal amount)
    {
        var account = GetAccount(accountId);
        if (account == null || amount <= 0 || account.Balance < amount)
        {
            return false;
        }
        account.Balance = account.Balance + amount;
        account.Transaction.Add(new Transaction(amount, "Withdrawn", $"Withdrew {amount: C}"));
        return true;

    }


    public bool Transfer(Guid fromAccountId, Guid toAccountId, decimal amount)
    {
        var fromAccount = GetAccount(fromAccountId);
        var toAccount = GetAccount(toAccountId);

        if(fromAccount == null || toAccount == null || amount <=0 || fromAccount.Balance < amount)
        {
            return false;
        }
        fromAccount.Balance = fromAccount.Balance - amount;
        toAccount.Balance = toAccount.Balance + amount;

        fromAccount.Transaction.Add(new Transaction(amount, "Transfer", $"Transferred{amount: C} to {toAccount.AccountHolder}"));
        toAccount.Transaction.Add(new Transaction(amount, "Transafer", $"Recieved {amount:C} from {fromAccount.AccountHolder}"));
        return true;

    }

    public List<Transaction> GetTransactions(Guid accountId)
    {
        var account = GetAccount(accountId);
        return account?.Transaction ?? new List<Transaction>();
    }
}
