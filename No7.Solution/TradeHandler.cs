using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System;

namespace No7.Solution
{
    public class TradeHandler
    {
        #region private Fields
        private List<string> lines; //сохраняет линии, прочитанные с файла
        private List<ITrade> trades; // сохраняет трэйдс в отдельном листе
        private Stream stream; //поток
        private IReposytory reposytory; //репозиторий, в который нужно будет закидывать данные,
                                        //их может быть несколько поэтому есть общий интерфейс 
                                        //и потом при помощи полиморфного вызова вызывается 
                                        //нужный метод
        #endregion

        #region Public Api
        public void HandleTrades(Stream stream)
        {
            this.stream = stream;
            TradeRecord trade = new TradeRecord();
            StartTrade(trade);
            reposytory = new DataBaseSqlTable();
        }

        public void HandleTrades(Stream stream, IReposytory reposytory)
        {
            this.stream = stream;
            TradeRecord trade = new TradeRecord();
            StartTrade(trade);
            this.reposytory = reposytory;
        }
        //добавил этот конструктор для добавления нового репозитория
        //для хранения данных

        public void HandleTrades(Stream stream, ITrade trade)
        {
            this.stream = stream;
            StartTrade(trade);
            this.reposytory = new DataBaseSqlTable();
        }
        //добавил этот конструктор для добавления нового Trade
        //для хранения данных
        #endregion

        #region Private Methods
        //Метод, который начинает работу по сделке в 3 этапа: 
        //читать с файла, заполнение сделок и созхранение в базу данных
        private void StartTrade(ITrade trade)
        {
            FileReader(this.stream);
            FillTrades(trade);
            SaveIntoDatabase();
        }

        //Метод, который при помощи полиморфного вызого выхывает нужный метод для сохранения
        //данных в нужный репозиторий
        private void SaveIntoDatabase() => reposytory.SaveData(trades);

        //метод, который создает экземпляры сделок нужного формата
        //так же при помощи полиморфного вызова
        private void FillTrades(ITrade trade) => trade.FillData(this.lines);

        //Метод для чтения файла
        private void FileReader(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                string line;
                lines = new List<string>();
                trades = new List<ITrade>();
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
        }
        #endregion
    }
}
