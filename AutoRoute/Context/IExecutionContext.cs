using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRoute.Context
{
    public interface IExecutionContext
    {
        public IDictionary<string, object> Properties { get; }
        public IServiceProvider ServiceProvider { get; }
    }
}
