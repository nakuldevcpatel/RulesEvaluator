using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console.App.Abstracts.Specification
{
    public interface ISpecification
    {
        bool isSatisfiedBy(string jsonData);
        ISpecification and(ISpecification specification);
        ISpecification or(ISpecification specification);
        ISpecification not(ISpecification specification);
    }
}
