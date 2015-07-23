using RuleEngine.Abstracts;
using RuleEngine.Abstracts.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleEngine.Implementation.Specifications
{
    public class LessThan : NumericSpecificationExpression
    {
        public LessThan(IJsonParser jsonParser,
            ArithmeticExpression left,
            ArithmeticExpression right)
            : base(jsonParser, left, right) { }


        public override bool isSatisfiedBy(string jsonData)
        {
            return leftArithmeticExpression.calculate(jsonData) < rightArithmeticExpression.calculate(jsonData);
        }
    }

    public class Equals : NumericSpecificationExpression
    {
        public Equals(IJsonParser jsonParser,
            ArithmeticExpression left,
            ArithmeticExpression right)
            : base(jsonParser, left, right) { }


        public override bool isSatisfiedBy(string jsonData)
        {
            return leftArithmeticExpression.calculate(jsonData) == rightArithmeticExpression.calculate(jsonData);
        }
    }
}
