using App.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entity
{
    public class Wallet
    {
        public decimal Balance { get; private set; }

        public Wallet(decimal initialBalance)
        {
            this.Balance = initialBalance;
        }

        public void Deposit(decimal amount)
        {
            this.Balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            if (amount > this.Balance)
            {
                throw new InsufficientFundsException("Not enough funds in the wallet.");
            }

            this.Balance -= amount;
        }

        public bool HasSufficientFunds(decimal amount)
        {
            return this.Balance >= amount;
        }
    }
}
