using AutoRoute.Abstarctions.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRoute.Routing
{
    public interface IRouteBuider
    {
        public IRoute AddStep(IInputCommands command);
        public IRoute AddStep(ICollection<IInputCommands> commands);
    }
}
