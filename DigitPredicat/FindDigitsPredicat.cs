using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitPredicat
{
    public class FindDigitsPredicat
    {
        /// <summary>
        /// Method for verification include number in element input array
        /// </summary>
        /// <param name="inputArray">input array</param>
        /// <param name="inputPredicateNumber">array numbers - filters</param>
        /// <returns>output array with element that contains filters numbers</returns>
        public static int[] FilterDigit(int[] inputArray, int inputPredicateNumber)
        {
            if (inputArray == null)
            {
                throw new ArgumentNullException($"Argument {nameof(inputArray)} is null");
            }

            if (inputArray.Length == 0)
            {
                throw new ArgumentOutOfRangeException($"Argument`s {nameof(inputArray)} length is 0");
            }

            if (inputPredicateNumber < 0 )
            {
                throw new ArgumentOutOfRangeException($"Digit`s filter number must be greater than 0");
            }

            Collection<int> rezaltCollection = new Collection<int>();

            for(int i = 0; i < inputArray.Length; i++)
            {
                if (FunctionPredicate(inputArray[i], inputPredicateNumber))
                {
                    rezaltCollection.Add(inputArray[i]);
                }
            }

            return rezaltCollection.ToArray();

        }

        /// <summary>
        /// Method for change input number in digit array
        /// </summary>
        /// <param name="itemNumber">number for change in digit array</param>
        /// <returns>array digit</returns>
        private static int[] DigitInArray(int itemNumber)
        {
            Collection<int> collectHelper = new Collection<int>();

            do
            {
                collectHelper.Add(itemNumber % 10);

                itemNumber /= 10;
            }
            while (itemNumber > 0);

            return collectHelper.ToArray();
        }

        /// <summary>
        /// Function predicat for find item which contain inputPredicateDigit
        /// </summary>
        /// <param name="inputSourceNumber">item</param>
        /// <param name="inputPredicateDigit">number predicat</param>
        /// <returns>true if item contain inputPredicateDigit</returns>
        public static bool FunctionPredicate(int inputSourceNumber, int inputPredicateDigit)
        {
            var arraySourceNumber = DigitInArray(inputSourceNumber);

            var arrayDigitFilter = DigitInArray(inputPredicateDigit);

            if (arraySourceNumber.Length < arrayDigitFilter.Length)
            {
                return false;
            }
            else
            {
                if (arraySourceNumber.Length == arrayDigitFilter.Length)
                {
                    return (inputSourceNumber ^ inputPredicateDigit) == 0;
                }
                else
                {
                    int countIteration = 0, countHelper = 0; 
                    var numberHelper = 0.0;

                    while (countIteration + arrayDigitFilter.Length <= arraySourceNumber.Length)
                    {
                        for (int j = countIteration; j < arrayDigitFilter.Length + countIteration; j++)
                        {
                            countHelper++;
                            numberHelper +=  arraySourceNumber[j] / Math.Pow(10, countHelper);
                        }

                        numberHelper *= Math.Pow(10, countHelper);

                        var resultNumber = (int) numberHelper;

                        if ((resultNumber ^ inputPredicateDigit) == 0)
                        {
                            return true;
                        }

                        countIteration++;
                        numberHelper = 0;
                        countHelper = 0;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Private method to determine whether a number in an element of an array
        /// </summary>
        /// <param name="itemArray">element of array</param>
        /// <param name="number">number - filter</param>
        /// <returns>true if element of array contains digit and false if he isn`t contains digit </returns>
        public static bool IsNumber(int itemArray, int number)
        {
            if (!FunctionPredicate(itemArray, number))
            {
                return false;
            }

            return true;
        }
    }
}
