using AutoRoute.Abstarctions.Outputs;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRoute.Core.Commands
{
    public interface IClickCommands
    {
        public IOutputType Click(string PathToFileWithCoordinates);
        public Task<IOutputType> ClickAsync(string PathToFileWithCoordinates);
        public IOutputType Click(int X, int Y);
        public Task<IOutputType> ClickAsync(int X, int Y);
    }
}
