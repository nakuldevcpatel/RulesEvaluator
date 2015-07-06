using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleEngine.Implementation
{
    public class RuleSetBreakDown
    {
        public bool result {get; protected set;}
        public Hashtable breakdown { get; protected set; }
        public RuleSetBreakDown(bool result, Hashtable breakdown)
        {
            this.result = result;
            this.breakdown = breakdown;
        }

    }
}
