using AutoRoute.Abstarctions.Inputs;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRoute.Context
{
    public interface IAutomationContext
    {
        public ICollection<IInputType> Inputs { get; } 

    }
}
