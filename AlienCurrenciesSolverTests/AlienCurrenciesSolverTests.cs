using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlienCurrenciesSolverNamespace;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace AlienCurrenciesSolverNamespace.Tests
{
    [TestClass()]
    public class AlienCurrenciesSolverTests
    {
        public static AlienCurrenciesSolver solver;
		[ClassInitialize()]
		public static void init(TestContext t)
        {
	        solver=new AlienCurrenciesSolver();
		}

        [TestMethod()]
        public void toLowestDenominationTest_2Dollars_5cents()
        {
            int[] conversions = { 100 },price={2,5};
            Assert.AreEqual(205,solver.toLowestDenomination(conversions,price));

        }

        [TestMethod()]
        public void toLowestDenominationTest_Decimal312()
        {
            int[] conversions = { 10,10 }, price = { 3,1,2 };
            Assert.AreEqual(312, solver.toLowestDenomination(conversions, price));

        }

        [TestMethod()]
        public void toLowestDenominationTest_WeirdConversions312()
        {
            int[] conversions = { 5, 8,9 }, price = { 3, 1, 2,8 };
            //5 of first is 15 of second,so 16 of second total, which is
            //16*8=128 of third, so 130 total of thrid, which is 130*9=1170 of 4th
            //so 1178 4th total
            Assert.AreEqual(1178, solver.toLowestDenomination(conversions, price));
        }

        [TestMethod()]
        public void solveOneTest_DecimalPrices()
        {
            int[] conversions = { 10, 10 };
            int[][] prices = new int[][]{ new int[]{3, 1, 2},new int[]{5,2,6},new int[]{1,9,8} };
            Assert.AreEqual(328, solver.solveOne(conversions,prices,3));
        }

        [TestMethod()]
        public void solveOneTest_SentCase1()
        {
            int[] conversions = { 2 };
            int[][] prices = new int[][] { new int[] { 2,0 }, new int[] { 0,5 }};
            Assert.AreEqual(1, solver.solveOne(conversions, prices, 2));
        }

        [TestMethod()]
        public void solveOneTest_SentCase2()
        {
            int[] conversions = { 3,5 };
            int[][] prices = new int[][] { new int[] { 1, 1, 1 }, new int[] { 3, 0, 0 }, new int[] { 1, 10, 0 } };
            Assert.AreEqual(44, solver.solveOne(conversions, prices, 3));
        }

        [TestMethod()]
        [ExpectedException(typeof(FormatException))]
        public void getDataSetsTest_ThrowsExceptionFor_BadFormat()
        {
           //Last price only one value
           string[] input={"2","2 2","2","2 0","0"};
           solver.getDataSets(input);
        }

        [TestMethod()]
        [ExpectedException(typeof(FormatException))]
        public void getDataSetsTest_ThrowsExceptionFor_BadNumberFormat()
        {
            //Cant parse j
            string[] input = { "1", "2 2", "j", "2 0", "0 5" };
            solver.getDataSets(input);
        }

        [TestMethod()]
        public void getDataSetsTestGiven_Set1()
        {
            string[] input = { "1", "2 2", "2", "2 0", "0 5" };

            Assert.AreEqual(2,solver.getDataSets(input)[0].PriceArrays[0][0]);
        }

        [TestMethod()]
        public void solveTest_ReturnNull_InputNull()
        {
            Assert.IsNull(solver.solve(null));
        }

        [TestMethod()]
        public void solveTest_ReturnNull_InputBadFormat()
        {
            string[] input = { "1", "2 2", "k", "2 0", "0 5" };
            Assert.IsNull(solver.solve(input));
        }

        [TestMethod()]
        public void solveTest_ReturnNull_InputIncorrectNumberOfPrices()
        {
            string[] input = { "1", "2 2", "2", "2 0" };
            Assert.IsNull(solver.solve(input));
        }

        [TestMethod()]
        public void solveTest_ReturnCorrectOutput()
        {
            string[] input = { "1", "2 2", "2", "2 0", "0 5" };
            string expected = "Data Set 1:\n1\n";
            Assert.AreEqual(expected,solver.solve(input));
        }
    }
}
