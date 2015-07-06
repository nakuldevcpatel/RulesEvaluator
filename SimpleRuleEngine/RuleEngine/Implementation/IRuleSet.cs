using RuleEngine.Abstracts.Specification;
using RuleEngine.Implementation.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleEngine.Abstracts
{
    public interface IRuleSet
    {
        List<CompositeSpecification> GetRules();
        void AddRule(CompositeSpecification rule);
        bool isSatisfiedBy(string jsonData);
    }
}
