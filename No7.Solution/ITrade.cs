using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace No7.Solution
{
    //интерйес для сделок
    public interface ITrade
    {
        List<ITrade> FillData(List<string> lines);
    }
}
