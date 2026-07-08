using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRoute.Abstarctions.Inputs
{
    public class InputTask : IInputType
    {
        public string Name { get; set; }
        public bool IsRunning { get; set; }
        public Task Procces { get; set; }
        public CancellationToken CancellationToken { get; set; }
    }
}
