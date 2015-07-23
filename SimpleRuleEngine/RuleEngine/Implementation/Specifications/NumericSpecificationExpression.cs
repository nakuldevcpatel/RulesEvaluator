using RuleEngine.Abstracts;
using RuleEngine.Abstracts.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleEngine.Implementation.Specifications
{
    public abstract class NumericSpecificationExpression : CompositeSpecification
    {
        protected ArithmeticExpression leftArithmeticExpression;
        protected ArithmeticExpression rightArithmeticExpression;
        public NumericSpecificationExpression(IJsonParser jsonParser,
            ArithmeticExpression leftArithmeticExpression,
            ArithmeticExpression rightArithmeticExpression) : base(jsonParser)
        {
            this.leftArithmeticExpression = leftArithmeticExpression;
            this.rightArithmeticExpression = rightArithmeticExpression;
        }
    }
}
