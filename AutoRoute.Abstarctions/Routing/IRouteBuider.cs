using AutoRoute.Abstarctions.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRoute.Abstarctions.Routing
{
    public interface IRouteBuider
    {
        public IRouteBuider AddStep(IAutomationCommand command);
        public IRoute Build();
    }
}
