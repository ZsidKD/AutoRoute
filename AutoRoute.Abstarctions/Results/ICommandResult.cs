using AutoRoute.Abstarctions.Outputs;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRoute.Abstarctions.Results
{
    public interface ICommandResult
    {
        public bool IsSuccess { get; }
        public string Message { get; }
        public IOutputType Output { get; }
    }
}
