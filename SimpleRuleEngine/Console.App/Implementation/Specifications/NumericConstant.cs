using Console.App.Abstracts.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console.App.Implementation.Specifications
{
    public class NumericConstant : ArithmeticExpression
    {
        private readonly float value;
        public NumericConstant(float value)
        {
            this.value = value;
        }

        public float calculate(string jsonData)
        {
            return this.value;
        }
    }
}
