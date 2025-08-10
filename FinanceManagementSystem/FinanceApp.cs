using System;
using System.Collections.Generic;

public class FinanceApp
{
    private List<Transaction> _transactions = new List<Transaction>();

    public void Run()
    {
        var savingsAccount = new SavingsAccount("SA-001", 1000m);

        var t1 = new Transaction(1, DateTime.Now, 120m, "Groceries");
        var t2 = new Transaction(2, DateTime.Now, 250m, "Utilities");
        var t3 = new Transaction(3, DateTime.Now, 800m, "Entertainment");

        ITransactionProcessor p1 = new MobileMoneyProcessor();
        ITransactionProcessor p2 = new BankTransferProcessor();
        ITransactionProcessor p3 = new CryptoWalletProcessor();

        p1.Process(t1);
        p2.Process(t2);
        p3.Process(t3);

        savingsAccount.ApplyTransaction(t1);
        savingsAccount.ApplyTransaction(t2);
        savingsAccount.ApplyTransaction(t3);

        _transactions.AddRange(new[] { t1, t2, t3 });
    }

    public static void Main()
    {
        var app = new FinanceApp();
        app.Run();
    }
}
