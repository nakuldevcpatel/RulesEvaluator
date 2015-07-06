using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleEngine.Abstracts
{
    public interface IRuleCompiler
    {
        IRuleSet GetCompiledRule(string rule);
    }
}
