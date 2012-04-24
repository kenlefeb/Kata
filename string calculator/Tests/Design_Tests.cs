using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeFebvre.Kata.StringCalculator;
using NUnit.Framework;

namespace Tests.LeFebvre.Kata.StringCalculator
{
    [TestFixture]
    public class Design_Tests
    {
        // The method can take 0, 1 or 2 numbers, 
        // and will return their sum 
        // (for an empty string it will return 0) 
        // for example “” or “1” or “1,2”

        [Test]
        public void Add_method_returns_zero_for_empty_string()
        {
            // Arrange
            var expected = 0;

            // Act
            var actual = new Calculator().Add(String.Empty);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Add_method_returns_one_for_the_number_one()
        {
            // Arrange
            var expected = 1;

            // Act
            var actual = new Calculator().Add("1");

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Add_method_returns_two_for_the_number_two()
        {
            // Arrange
            var expected = 2;

            // Act
            var actual = new Calculator().Add("2");

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Datapoints]
        public string[] parameters = new string[] { "", "1", "2", "1,2", "2,3" };
        [Datapoints]
        public int[] answers = new int[] { 0, 1, 2, 3, 5 };

        [Theory]
        public void Add_method_returns_sum_of_specified_numbers(string numbers, int answer)
        {
            // Arrange
            var values = (string.IsNullOrEmpty(numbers)) ? new int[] { 0,0} : numbers.Split(new char[] { ',' }).Select<string, int>(number => int.Parse(number));
            var expected = values.Sum();

            // Act
            var actual = new Calculator().Add(numbers);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
