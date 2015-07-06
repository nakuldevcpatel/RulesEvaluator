using RuleEngine.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleEngine.Implementation.Specifications
{
    public class IsTrue : CompositeSpecification
    {
        private string jsonPathExpression;
        public IsTrue(IJsonParser jsonParser, string jsonPathExpression) : base(jsonParser)
        {
            this.jsonPathExpression = jsonPathExpression;
        }
        public override bool isSatisfiedBy(string jsonData)
        {
            return bool.Parse(jsonParser.query(jsonData, jsonPathExpression));
        }
    }
}
