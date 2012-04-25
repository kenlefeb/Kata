using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeFebvre.Kata.StringCalculator
{
    public class Calculator
    {
        public Calculator() : this(new string[] { "," }) { }

        public Calculator(string[] delimiters)
        {
            this.delimiters = delimiters;
        }

        private string[] delimiters;

        public int Add(string numbers)
        {
            defineDelimiters(ref numbers);

            if (string.IsNullOrEmpty(numbers))
                return 0;

            var values = parseNumbers(numbers);
            checkForNegatives(values);

            return values.Sum();
        }

        private IEnumerable<int> parseNumbers(string numbers)
        {
            var strings = numbers.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            var values = numbers.Split(this.delimiters, StringSplitOptions.RemoveEmptyEntries)
                            .Select<string, int>(item => int.Parse(item))
                            .Where(value => value <= 1000);
            return values;
        }

        private void defineDelimiters(ref string numbers)
        {
            if (numbers.StartsWith("//"))
            {
                var newline = numbers.IndexOf('\n');
                if (numbers.Substring(2).StartsWith("[") && numbers.Substring(newline - 1).StartsWith("]"))
                {
                    var delimiterDefinitions = numbers.Substring(3, (newline - 4));
                    delimiters = delimiterDefinitions.Split(new string[] { "][" }, StringSplitOptions.RemoveEmptyEntries);
                }
                else
                {
                    var strings = new List<string>();
                    foreach (var character in numbers.Substring(2, (newline - 2)).ToCharArray())
                        strings.Add(character.ToString());
                    delimiters = strings.ToArray();
                }
                numbers = numbers.Substring(newline + 1);
            }
        }

        private static void checkForNegatives(IEnumerable<int> values)
        {
            var negatives = values.Where(nbr => nbr < 0);
            if (negatives.Count() > 0)
            {
                var builder = new StringBuilder();
                foreach (var negative in negatives)
                {
                    if (builder.Length > 0)
                        builder.Append(", ");
                    builder.Append(negative);
                }
                throw new Exception(string.Format("Negatives not allowed: {0}", builder.ToString()));
            }
        }
    }
}
