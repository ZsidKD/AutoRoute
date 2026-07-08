using AutoRoute.Abstarctions.Outputs;
using AutoRoute.Abstarctions.Locators;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRoute.Core.Commands.OutputCommands
{
    public interface IOutputHtml
    {
        public IOutputType Output(IOutputType outputType);
        public OutputHtml OutputHtml(string PathToFileWithHtml);
        public Task<OutputHtml> OutputHtml(Uri Uri, CancellationToken cancellationToken);
        public Task<OutputHtml> OutputHtml(string url, CancellationToken cancellationToken);
    }
}
