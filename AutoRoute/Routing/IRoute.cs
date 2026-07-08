using AutoRoute.Abstarctions.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRoute.Routing
{
    public interface IRoute
    {
        IReadOnlyList<IInputCommands> Commands { get; }
    }
}
