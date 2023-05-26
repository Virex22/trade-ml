namespace App.Exception
{
    public class InsufficientFundsException : System.Exception
    {
        public InsufficientFundsException() : base() { }

        public InsufficientFundsException(string message) : base(message) { }
    }
}
