using Console.App.Abstracts;
using Console.App.Implementation.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console.App.Implementation
{
    public class RuleSet : IRuleSet
    {
        private List<CompositeSpecification> rules;
        public RuleSet()
        {
            rules = new List<CompositeSpecification>();
        }
        public List<CompositeSpecification> GetRules()
        {
            return rules;
        }
        public void AddRule(CompositeSpecification rule)
        {
            this.rules.Add(rule);
        }
        public bool isSatisfiedBy(string jsonData)
        {
            bool result = true;
            foreach (var rule in rules)
            {
                if (!rule.isSatisfiedBy(jsonData))
                {
                    result = false;
                    break;
                }
            }
            return result;
        }
    }
}
