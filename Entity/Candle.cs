﻿namespace App.Entity
{
    public class Candle
    {
        public DateTimeOffset OpenTime;
        public decimal Open;
        public decimal High;
        public decimal Low;
        public decimal Close;
        public DateTimeOffset CloseTime;

        public Candle()
        {

        }
    }
}
