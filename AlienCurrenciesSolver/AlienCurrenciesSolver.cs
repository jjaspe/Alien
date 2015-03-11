using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlienCurrenciesSolverNamespace
{

    /// <summary>
    /// Class to hold all the data we need for calculations
    /// in one object
    /// </summary>
    public class dataSet
    {
        int prices;

        public int Prices
        {
            get { return prices; }
            set { prices = value; }
        }
        int[] conversions;

        public int[] Conversions
        {
            get { return conversions; }
            set { conversions = value; }
        }
        int[][] priceArrays;

        public int[][] PriceArrays
        {
            get { return priceArrays; }
            set { priceArrays = value; }
        }
        public dataSet(int prices,int[] conversions,int[][]priceArrays)
        {
            this.prices = prices;
            this.conversions = conversions;
            this.priceArrays = priceArrays;
        }
    }


    /// <summary>
    /// Handles all the calculations for the actual problem
    /// </summary>
    public class AlienCurrenciesSolver
    {
        /// <summary>
        ///  Gives the price using the lowest denomination coin only.
        ///  Assumes the lowest denomination is at the end of the arrays
        /// </summary>
        /// <param name="conversions">Conversion factors between denoms.</param>
        /// <param name="price">Price using all denoms.</param>
        /// <returns>The price in lowest denomination coins</returns>
        public int toLowestDenomination(int[] conversions,int[] price)
        {
            int sum = 0;
            //Each step turns the coins one denom. lower,
            //by taking how many i-th denom coins we had from before, adding the
            //ones on the price, then converting to i+1th denom by multiplying the conversion factor.
            for (int i = 0; i < conversions.Length; i++)
            {
                sum = (sum+price[i])* conversions[i];
            }
            //Add the last one from price,lowest denom so no need to convert
            sum += price[price.Length - 1];

            return sum;
        }

        /// <summary>
        /// Gets the difference, for one data set, between the highest price
        /// and lowest price, given in the smallest denomination
        /// </summary>
        /// <param name="conversions"></param>
        /// <param name="prices"></param>
        /// <param name="numberOfPrices"></param>
        /// <returns></returns>
        public int solveOne(int[] conversions,int[][] prices,int numberOfPrices)
        {
            int[] pricesInLowest = new int[numberOfPrices];
            int lowest=-1, highest = -1;

            for (int i = 0; i < prices.Length; i++)
            {
                pricesInLowest[i] = toLowestDenomination(conversions, prices[i]);
                if (highest == -1 || pricesInLowest[i] > highest)
                    highest = pricesInLowest[i];
                if (lowest == -1 || pricesInLowest[i] < highest)
                    lowest = pricesInLowest[i];
            }

            return highest - lowest;
        }

        /// <summary>
        /// Parses strings into useful data.
        /// Throws exception if there is something badly formatted or 
        /// there arent the right number of elements per line
        /// 
        /// Assumes each element of input is a line
        /// First line:number of data sets
        /// 
        /// Second: number of denoms" "number of prices"
        /// Third: denom conversion
        /// 4-i:Prices
        /// 
        /// Repeat from second 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public dataSet[] getDataSets(string[] input)
        {
            dataSet[] sets=null;
            int dataSetsLength = Int32.Parse(input[0]);
            sets= new dataSet[dataSetsLength];
            int denoms, prices,nextSecond=1;
            int[] conversions;
            int[][] priceArrays;
            string[] splitLine;
            for(int i=0;i<dataSetsLength;i++)
            {
                //Get number of denoms and prices
                splitLine = input[nextSecond].Split(' ');
                denoms = Int32.Parse(splitLine[0]);
                prices = Int32.Parse(splitLine[1]);

                //Get denom array,move nextSecond one line down
                splitLine = input[++nextSecond].Split(' ');
                if (splitLine.Length != denoms - 1)
                    throw new FormatException(
                        "Conversion line doesn't have the right number of elements.");
                conversions = new int[splitLine.Length];
                    
                for (int j = 0; j < splitLine.Length; j++)
                {
                    conversions[j]=Int32.Parse(splitLine[j]);
                }

                //Get Prices,move next second to beginning of price lines
                priceArrays = new int[prices][];
                nextSecond++;
                for (int k = 0; k < prices; k++)
                {
                    //move down price lines using k, starting from nextSecond
                    splitLine = input[nextSecond + k].Split(' ');
                        
                    if (splitLine.Length != denoms)
                        throw new FormatException("Number of prices not the right length.");
                    priceArrays[k] = new int[denoms];
                    //Read prices
                    for (int j = 0; j < splitLine.Length; j++)
                    {
                        priceArrays[k][j] = Int32.Parse(splitLine[j]);
                    }
                }
                //Move nextSecond to the next data set's line with the denom and price numbers
                nextSecond +=prices;

                //Create data set with values read
                sets[i] = new dataSet(prices, conversions, priceArrays);
            }

            return sets;
        }

        /// <summary>
        /// Gives the highest difference in price for each data set.
        /// Output has format
        /// Data set 1:
        /// [highest difference]
        /// Data set 2:
        /// [highest difference]
        /// ...
        /// </summary>
        /// <param name="input"></param>
        /// <returns>output in above format or null if there were any problems or input was null</returns>
        public string solve(string[] input)
        {
            dataSet[] sets=null;
            if (input == null)
                return null;

            try
            {
                sets = getDataSets(input);
            }catch(FormatException fe)
            {
                Console.WriteLine("Format Exception\n" + fe.Message);
                return null;
            }catch(IndexOutOfRangeException ie)
            {
                Console.WriteLine("Index out of bounds Exception\n" +
                    "Most likely the number of prices specified did not match" +
                    " the number of price lines for a data set");
                return null;
            }
            
            string output = "";
            for (int i = 0; i < sets.Length; i++)
            {
                output += "Data Set " + (i+1) + ":\n";
                output += solveOne(sets[i].Conversions, sets[i].PriceArrays, sets[i].Prices) + "\n";
            }
            return output;
        }
    }
}
