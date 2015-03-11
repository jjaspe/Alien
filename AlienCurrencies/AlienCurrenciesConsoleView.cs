using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlienCurrencies
{
    /// <summary>
    /// This implementation loads data from a textfile and 
    /// displays output on console
    /// </summary>
    public class AlienCurrenciesConsoleView:IAlienCurrenciesView
    {
        AlienCurrenciesController controller;

        public string[] getInput()
        {
            StreamReader reader = new StreamReader("../../Data.txt");
            return reader.ReadToEnd().Split('\n');
        }

        public void showOutput(string output)
        {
            Console.WriteLine(output);
        }

        public AlienCurrenciesController Controller
        {
            get
            {
                return controller;
            }
            set
            {
                controller = value;
            }
        }
    }
}
