using RuleEngine.Abstracts;
using RuleEngine.Abstracts.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleEngine.Implementation.Specifications
{
    public class RealArithmeticExpression : ArithmeticExpression
    {
        ArithmeticOperatorType operatorType;
        ArithmeticExpression left;
        ArithmeticExpression right;
        public RealArithmeticExpression(ArithmeticOperatorType operatorType,
            ArithmeticExpression left, ArithmeticExpression right)
        {
            this.operatorType = operatorType;
            this.left = left;
            this.right = right;
        }

        public float calculate(String jsonData)
        {
            switch (operatorType)
            {
                case ArithmeticOperatorType.add: return left.calculate(jsonData) + right.calculate(jsonData);
                case ArithmeticOperatorType.subtract: return left.calculate(jsonData) - right.calculate(jsonData);
                case ArithmeticOperatorType.multiply: return left.calculate(jsonData) * right.calculate(jsonData);
                case ArithmeticOperatorType.divide: return left.calculate(jsonData) / right.calculate(jsonData);
            }
            throw new Exception("Could not calculate");

        }
    }
}
