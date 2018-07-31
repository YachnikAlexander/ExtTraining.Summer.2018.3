using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace No7.Solution
{
    //один из экземпляров ITrade
    public class TradeRecord : ITrade
    {
        public string DestinationCurrency { get; private set; }
        public float Lots { get; private set; }
        public decimal Price { get; private set; }
        public string SourceCurrency { get; private set; }
        private readonly float LOTSIZE = 100000f; //сменил на константу, так как это не поле, которое хранит данные, а число, которое используется при вычислении
        //Не забыть сменить на ридонли

        public TradeRecord(string destinationCurrency, float lots, decimal price, string sourceCurrency)
        {
            this.DestinationCurrency = destinationCurrency;
            this.Lots = lots;
            this.Price = price;
            this.SourceCurrency = sourceCurrency;
        }

        public TradeRecord()
        {
            this.DestinationCurrency = default(string);
            this.Lots = default(float);
            this.Price = default(decimal);
            this.SourceCurrency = default(string);
        }
        //метод для заполнения данных 
        public List<ITrade> FillData(List<string> lines)
        {
            List<TradeRecord> trades = new List<TradeRecord>();
            int lineCount = 1;
            foreach (var line in lines)
            {
                if(line == null)
                {
                    break;
                }
                string[] fields = line.Split(',');

                ValidationFields(fields, lineCount);

                bool amountTryParse = int.TryParse(fields[1], out int tradeAmount);
                bool priceTryPase = decimal.TryParse(fields[2], out var tradePrice);

                string sourceCurrencyCode = fields[0].Substring(0, 3);
                string destinationCurrencyCode = fields[0].Substring(3, 3);
                var lots = tradeAmount / LOTSIZE;

                var trade = new TradeRecord(destinationCurrencyCode, lots, tradePrice, sourceCurrencyCode);

                trades.Add(trade);

                lineCount++;

            }
            return null;
        }

        private void ValidationFields(string[] fields, int lineCount)
        {
            if (fields.Length != 3)
            {

                System.Console.WriteLine("WARN: Line {0} malformed. Only {1} field(s) found.", lineCount, fields.Length);
            }

            if (fields[0].Length != 6)
            {
                System.Console.WriteLine("WARN: Trade currencies on line {0} malformed: '{1}'", lineCount, fields[0]);
            }

            if (!int.TryParse(fields[1], out var tradeAmount))
            {
                System.Console.WriteLine("WARN: Trade amount on line {0} not a valid integer: '{1}'", lineCount, fields[1]);
            }

            if (!decimal.TryParse(fields[2], out var tradePrice))
            {
                System.Console.WriteLine("WARN: Trade price on line {0} not a valid decimal: '{1}'", lineCount, fields[2]);
            }
        }
    }
}
