using Console.App.Abstracts;
using Console.App.Abstracts.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console.App.Implementation.Specifications
{
    public class NumericField : ArithmeticExpression
    {
        private IJsonParser parser;
        private string jsonPathExpression;
        public NumericField(IJsonParser jsonParser, string jsonPathExpression)
        {
            this.parser = jsonParser;
            this.jsonPathExpression = jsonPathExpression;
        }

        public float calculate(string jsonData)
        {
            string fieldValueAsString = parser.query(jsonData, jsonPathExpression);
            if (!string.IsNullOrEmpty(fieldValueAsString))
                return float.Parse(fieldValueAsString);

            throw new ArgumentException("Unable to calculate field name.", jsonPathExpression);
        }
    }
}
