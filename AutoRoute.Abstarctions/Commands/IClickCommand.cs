using AutoRoute.Abstarctions.Locators;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRoute.Abstarctions.Commands
{
    public interface IClickCommand : IAutomationCommand
    {
        public ILocator Target { get; set; }
    }
}
