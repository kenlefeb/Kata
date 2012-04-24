using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeFebvre.Kata.StringCalculator
{
    public class Calculator
    {
        public int Add(string numbers)
        {
            if (string.IsNullOrEmpty(numbers))
                return 0;

            var values = numbers.Split(new char[] { ',' }).Select<string, int>(item => int.Parse(item));
            return values.Sum();
        }
    }
}
