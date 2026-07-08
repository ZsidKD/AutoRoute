using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRoute.Abstarctions.Inputs
{
    public class InputImage : IInputType
    {
        public string Name { get; set; }
        public byte[] Data { get; set; }
        public string PathToImage { get; set; }

    }
}
