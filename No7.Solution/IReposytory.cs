using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace No7.Solution
{
    //интерфейс для репоизитория
    public interface IReposytory
    {
        void SaveData(List<ITrade> trades);
    }
}
