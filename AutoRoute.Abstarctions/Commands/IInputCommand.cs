using AutoRoute.Abstarctions.Inputs;
using AutoRoute.Abstarctions.Locators;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRoute.Abstarctions.Commands
{
    public interface IInputCommand : IAutomationCommand
    {
        public ILocator Target { get; set; }
        public IInputType InputType { get; set; }
    }
}
