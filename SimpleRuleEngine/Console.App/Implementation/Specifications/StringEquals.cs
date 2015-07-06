using Console.App.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console.App.Implementation.Specifications
{
    public class StringEquals : StringSpecificationExpression
    {
        public StringEquals(IJsonParser jsonParser, string jsonPathExpression, string value) 
            : base(jsonParser, jsonPathExpression, value) 
        { }
        public override bool isSatisfiedBy(string jsonData)
        {
            return jsonParser.query(jsonData, jsonPathExpression).Equals(value);
        }
    }
}
