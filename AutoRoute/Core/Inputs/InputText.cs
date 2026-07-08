using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRoute.Abstarctions.Inputs
{
    public class InputText : IInputType
    {
        public string Name { get; set; }
        public string? Text { get; set; }
        public string? PathToFileWithText { get; set; }

    }
}
