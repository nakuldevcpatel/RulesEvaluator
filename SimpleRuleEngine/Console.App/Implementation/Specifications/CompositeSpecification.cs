using Console.App.Abstracts;
using Console.App.Abstracts.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console.App.Implementation.Specifications
{
    public abstract class CompositeSpecification : ISpecification
    {
        public IJsonParser jsonParser { get; private set; }
        public CompositeSpecification(IJsonParser jsonParser)
        {
            this.jsonParser = jsonParser;
        }

        public abstract bool isSatisfiedBy(string jsonData);

        public ISpecification and(ISpecification specification)
        {
            return new AndSpecification(jsonParser,this,specification);
        }

        public ISpecification or(ISpecification specification)
        {
            return new OrSpecification(jsonParser,this,specification);
        }

        public ISpecification not(ISpecification specification)
        {
            return new NotSpecification(jsonParser, specification);
        }
    }
}
