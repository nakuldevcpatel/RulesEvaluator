﻿using Console.App.Abstracts;
using Console.App.Abstracts.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console.App.Implementation.Specifications
{
    public class OrSpecification : CompositeSpecification
    {
        ISpecification leftSpecification;
        ISpecification rightSpecification;
        public OrSpecification(IJsonParser jsonParser, ISpecification left, ISpecification right) : base(jsonParser)
        {
            this.leftSpecification = left;
            this.rightSpecification = right;
        }
        public override bool isSatisfiedBy(string jsonData)
        {
            return leftSpecification.isSatisfiedBy(jsonData) || rightSpecification.isSatisfiedBy(jsonData);
        }
    }
}
