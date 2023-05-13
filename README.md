﻿# ML Trade - Machine Learning for Trading

## How to use

### Install

Compile C# project : 
```bash
dotnet build
```

### Run

Run C# project : 
```bash
dotnet run
```

### Configuration

Configuration file is located in `Config.json` file.
```json
{
  "symbol": "BTCUSDT",
  "interval": "5m",
  "cacheType": "File",
  "cachePath": "data/binance/cache/",
  "reportPath":  "data/report/",
  "testIntervalDay": 10,
  "variationAmplitudeCoef": 3,
  "initialAmount": 1000,  
  "basedTradeAmountPercentage" : 10,
  "HistMinDataBufferLen":  100
}
```

#### Parameters

- `symbol` : Symbol to trade
- `interval` : Interval of the candles
- `cacheType` : Type of cache to use (File | Net | None)
- `cachePath` : Path of the cache
- `reportPath` : Path of the report
- `testIntervalDay` : Number of days to test
- `variationAmplitudeCoef` : Coefficient of the variation amplitude
- `initialAmount` : Initial amount to trade
- `basedTradeAmountPercentage` : Percentage of the total wallet to trade
- `HistMinDataBufferLen` : Minimum number of candle data before starting the trading analysis
