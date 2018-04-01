using System;
using NUnit.Framework;
using DigitPredicat;

namespace DigitPredicat.Test
{
    [TestFixture]
    public class FindDigitsPredicatTest
    {
        [Test]
        public void FindDigitsPredicat_With_Valid_Data()
        {
            var arrayForTest = new int[100000];
            Random random = new Random(0);
            for (int itemArray = 0; itemArray < arrayForTest.Length; itemArray++)
            {
                arrayForTest[itemArray] = random.Next(0, 100000);
            }

            var outputArray = FindDigitsPredicat.FilterDigit(arrayForTest, 222);

            for (int itemArray = 0; itemArray < outputArray.Length; itemArray++)
            {
                Assert.IsTrue(FindDigitsPredicat.IsNumber(outputArray[itemArray], 222));
            }
        }

        /// <summary>
        /// Test method FindDigitsPredicat if expected ArgumentNullException
        /// </summary>
        /// <param name="inputArray">input array</param>
        /// <param name="number">filter number</param>
        [TestCase(null, 10)]
        public void FindDigitsPredicat_Expected_ArgumentNullException(int[] inputArray, int number)
            => Assert.Throws<ArgumentNullException>(() => FindDigitsPredicat.FilterDigit(inputArray, number));

        /// <summary>
        /// Test method FindDigitsPredicat if expected ArgumentOutOfRangeException
        /// </summary>
        /// <param name="inputArray">input array</param>
        /// <param name="number">filter number</param>
        [TestCase(new int[0], -1)]
        [TestCase(new int[5] { 12, 45, 6, 5, 8 }, -1)]
        public void FindDigitsPredicat_Expected_ArgumentOutOfRangeException(int[] inputArray, int number)
            => Assert.Throws<ArgumentOutOfRangeException>(() => FindDigitsPredicat.FilterDigit(inputArray, number));
    }
}
