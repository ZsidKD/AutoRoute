using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRoute.Abstarctions.Commands
{
    public interface IAutomationCommand
    {
        public string Name { get; set; }
        public TimeSpan TimeOut { get; set; }

    }
}
