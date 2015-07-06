using Console.App.Abstracts.Specification;
using Console.App.Implementation.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console.App.Abstracts
{
    public interface IRuleSet
    {
        List<CompositeSpecification> GetRules();
        void AddRule(CompositeSpecification rule);
        bool isSatisfiedBy(string jsonData);
    }
}
