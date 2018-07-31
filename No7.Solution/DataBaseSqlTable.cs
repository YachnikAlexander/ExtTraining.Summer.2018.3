using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace No7.Solution
{
    //один из экземпляров репозитория 
    class DataBaseSqlTable : IReposytory
    {
        private string connectionString;

        public void SaveData(List<ITrade> tradesInterface)
        {
            List<TradeRecord> trades = new List<TradeRecord>();
            for (int i = 0; i < tradesInterface.Count; i++)
            {
                trades[i] = (TradeRecord)tradesInterface[i];
            }
            SaveTradeRecord(trades);
        }

        private void SaveTradeRecord(List<TradeRecord> trades)
        { 
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    foreach (var trade in trades)
                    {
                        var command = connection.CreateCommand();
                        command.Transaction = transaction;
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = "dbo.Insert_Trade";
                        command.Parameters.AddWithValue("@sourceCurrency", trade.SourceCurrency);
                        command.Parameters.AddWithValue("@destinationCurrency", trade.DestinationCurrency);
                        command.Parameters.AddWithValue("@lots", trade.Lots);
                        command.Parameters.AddWithValue("@price", trade.Price);

                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                connection.Close();
            }
        }

        public DataBaseSqlTable()
        {

            connectionString = ConfigurationManager.ConnectionStrings["TradeData"].ConnectionString;
            Console.WriteLine(connectionString);
        }

        public DataBaseSqlTable(string configurationString)
        {
            this.connectionString = configurationString;
        }
    }
}
