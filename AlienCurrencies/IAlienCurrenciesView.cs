using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlienCurrencies
{
    public interface IAlienCurrenciesView
    {
        string[] getInput();
        void showOutput(string output);

        AlienCurrenciesController Controller
        {
            get;
            set;
        }
    }
}
