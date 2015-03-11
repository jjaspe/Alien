using AlienCurrenciesSolverNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlienCurrencies
{
    public class AlienCurrenciesController
    {
        IAlienCurrenciesView view;

        public IAlienCurrenciesView View
        {
            get { return view; }
            set { view = value; }
        }

        /// <summary>
        /// Gets input from view, makes safety checks, and tells view to show output
        /// </summary>
        public void index()
        {
            if (view == null)
                return;

            AlienCurrenciesSolver solver = new AlienCurrenciesSolver();
            string[] input=view.getInput();
            string output;
            if (input != null)
            {
                output = solver.solve(input);
                if (output != null)
                    view.showOutput(output);
            }
        }
    }
}
