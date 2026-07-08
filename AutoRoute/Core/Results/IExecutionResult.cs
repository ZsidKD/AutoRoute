using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRoute.Abstarctions.Results
{
    public interface IExecutionResult
    {
        public string source { get; }
        public Exception? exception { get; }
    }
}
