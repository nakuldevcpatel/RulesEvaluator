using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleEngine.Abstracts.Specification
{
    public interface ArithmeticExpression
    {
        float calculate(string jsonData);
    }
}
