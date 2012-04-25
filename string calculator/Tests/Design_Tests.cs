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

        [Test]
        public void Add_method_accepts_unknown_number_of_numbers()
        {
            // Allow the Add method to handle an unknown amount of numbers

            // Arrange
            var randomNumber = new Random().Next(1, 100);
            var numbersList = new List<int>(randomNumber);
            var builder = new StringBuilder();
            for (var index = 0; index < randomNumber; index++)
            {
                var thisNumber = new Random().Next(0, 100);
                numbersList.Add(thisNumber);
                if (builder.Length > 0)
                    builder.Append(",");
                builder.Append(thisNumber);
            }
            var numbers = builder.ToString();
            var expected = numbersList.Sum();

            // Act
            var actual = new Calculator().Add(numbers);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Add_method_accepts_newline_delimiters()
        {
            // Arrange
            var numbers = "1\n2";
            var expected = 3;

            // Act
            var actual = new Calculator(new string[] { ",", "\n" }).Add(numbers);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Add_method_accepts_optional_delimiter_line()
        {
            // Support different delimiters to change a delimiter, 
            // the beginning of the string will contain a separate line that looks like this:   
            // “//[delimiter]\n[numbers…]” for example “//;\n1;2” should return three where the default delimiter is ‘;’ . 
            // the first line is optional. all existing scenarios should still be supported

            // Arrange
            var numbers = "//;\n1;2";
            var expected = 3;

            // Act
            var actual = new Calculator().Add(numbers);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Add_method_throws_exception_when_negatives_are_passed()
        {
            // Calling Add with a negative number will throw an exception “negatives not allowed” - and the negative that was passed.
            // if there are multiple negatives, show all of them in the exception message 

            // Arrange
            var numbers = "-2,-3";
            var expected = "Negatives not allowed: -2, -3";
            var actual = string.Empty;

            // Act
            try
            {
                new Calculator().Add(numbers);
            }
            catch (Exception ex)
            {
                actual = ex.Message;
            }

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Add_method_ignores_numbers_higher_that_1000()
        {
            // Numbers bigger than 1000 should be ignored, so adding 2 + 1001  = 2

            // Arrange
            var numbers = "2,1001";
            var expected = 2;

            // Act
            var actual = new Calculator().Add(numbers);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Delimiters_may_be_any_length()
        {
            // Delimiters can be of any length with the following format: 
            // “//[delimiter]\n” for example: “//[***]\n1***2***3” should return 6

            // Arrange
            var numbers = "//[***]\n1***2***3";
            var expected = 6;

            // Act
            var actual = new Calculator().Add(numbers);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Multiple_delimiters_may_be_any_length()
        {
            // Allow multiple delimiters like this: 
            // “//[delim1][delim2]\n” for example “//[*][%]\n1*2%3” should return 6.

            // Arrange
            var numbers = "//[*][%]\n1*2%3";
            var expected = 6;

            // Act
            var actual = new Calculator().Add(numbers);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
