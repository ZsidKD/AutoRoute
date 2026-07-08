using AutoRoute.Abstarctions.Inputs;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRoute.Abstarctions.Commands
{
    public interface IWaitCommand
    {
        public TimeSpan TimeOut { get; set; }
    }
}
