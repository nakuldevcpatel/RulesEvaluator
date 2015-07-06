using Console.App.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console.App.Implementation.Specifications
{
    public class StringContains : StringSpecificationExpression
    {
        public StringContains(IJsonParser jsonParser, string jsonPathExpression, string value) :
            base(jsonParser, jsonPathExpression, value)
        {
        }

        public override bool isSatisfiedBy(string jsonData)
        {
            return this.jsonParser.query(jsonData, jsonPathExpression).Contains(value);
        }
    }
}
