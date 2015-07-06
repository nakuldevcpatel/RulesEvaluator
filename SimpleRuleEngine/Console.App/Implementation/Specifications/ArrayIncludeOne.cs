using Console.App.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console.App.Implementation.Specifications
{
    public class ArrayIncludeOne : CompositeSpecification
    {
        string jsonPathExpression;
        List<string> values;
        public ArrayIncludeOne(IJsonParser jsonParser, string jsonPathExpression, List<string> values) : base(jsonParser)
        {
            this.jsonPathExpression = jsonPathExpression;
            this.values = values;
        }
        public override bool isSatisfiedBy(string jsonData)
        {
            List<String> jsonPathExpressionResults = this.jsonParser.queryArray(jsonData, jsonPathExpression);

        foreach(string value in this.values) {
            if (jsonPathExpressionResults.Contains(value)) {
                return true;
            }
        }

        return false;
        }
    }
}
