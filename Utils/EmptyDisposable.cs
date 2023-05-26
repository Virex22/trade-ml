﻿namespace App.Utils
{
    public class EmptyDisposable : IDisposable
    {
        public static EmptyDisposable Instance = new EmptyDisposable();

        private EmptyDisposable() { }

        public void Dispose() { }
    }
}
