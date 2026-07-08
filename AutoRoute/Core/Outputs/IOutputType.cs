using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRoute.Abstarctions.Outputs
{
    public interface IOutputType
    {
        public string Name { get; }
        public Exception? exception { get; }
    }
}
