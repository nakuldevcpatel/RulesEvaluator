using Console.App.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console.App.Implementation.Specifications
{
    public abstract class StringSpecificationExpression : CompositeSpecification
    {
        protected string jsonPathExpression;
        protected string value;
        public StringSpecificationExpression(IJsonParser jsonParser, string jsonPathExpression, string value)
            : base(jsonParser)
        {
            this.jsonPathExpression = jsonPathExpression;
            this.value = value;
        }
    }
}
