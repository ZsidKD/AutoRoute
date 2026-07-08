using AutoRoute.Abstarctions.Inputs;
using AutoRoute.Abstarctions.Outputs;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRoute.Abstarctions.Commands
{
    public interface IAutomationCommands
    {
        public IOutputType Input(IInputType inputData);
        public Task<IOutputType> InputAsync(IInputType inputData, CancellationToken cancellationToken);
        public IOutputType Input(IInputType inputData, TimeSpan timeOut);
        public Task<IOutputType> InputAsync(IInputType inputData, TimeSpan timeOut, CancellationToken cancellationToken);
    }
}
