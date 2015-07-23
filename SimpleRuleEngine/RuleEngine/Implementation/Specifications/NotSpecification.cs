using RuleEngine.Abstracts;
using RuleEngine.Abstracts.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleEngine.Implementation.Specifications
{
    public class NotSpecification : CompositeSpecification
    {
        private ISpecification specification;
        public NotSpecification(IJsonParser jsonParser, ISpecification specification) : base(jsonParser)
        {
            this.specification = specification;
        }
        public override bool isSatisfiedBy(string jsonData)
        {
            return !this.specification.isSatisfiedBy(jsonData);
        }
    }
}
