using AutoRoute.Abstarctions.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRoute.Abstarctions.Routing
{
    public interface IRoute
    {
        IReadOnlyList<IAutomationCommand> Commands { get; }
    }
}
