using RuleEngine.Abstracts;
using RuleEngine.Abstracts.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleEngine.Implementation.Specifications
{
    public class TotalFromJsonPathExpression : ArithmeticExpression
    {
        private IJsonParser jsonParser;
        string jsonPathExpression;
        public TotalFromJsonPathExpression(IJsonParser jsonParser, string jsonPathExpression)
        {
            this.jsonParser = jsonParser;
            this.jsonPathExpression = jsonPathExpression;
        }

        public float calculate(String jsonData)
        {
            List<String> expressionResults = jsonParser.queryArray(jsonData, jsonPathExpression);

            float total = 0;
            foreach (string expressionResult in expressionResults)
            {
                total += float.Parse(expressionResult);
            }
            return total;
        }
    }
}
