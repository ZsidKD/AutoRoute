using AutoRoute.Abstarctions.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRoute.Abstarctions.Routing
{
    public interface IRouteBuider
    {
        public IRoute AddStep(IAutomationCommand command);
        public IRoute AddStep(ICollection<IAutomationCommand> commands);
    }
}
