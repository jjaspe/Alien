using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlienCurrenciesSolverNamespace;

namespace AlienCurrencies
{
    class Program
    {
        static void Main(string[] args)
        {
            IAlienCurrenciesView view = new AlienCurrenciesConsoleView();
            AlienCurrenciesController controller=new AlienCurrenciesController();
            view.Controller = controller;
            controller.View = view;

            controller.index();
            
        }
    }
}
