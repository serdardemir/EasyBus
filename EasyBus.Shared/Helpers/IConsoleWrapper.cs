using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyBus.Shared.Helpers
{
    internal interface IConsoleWrapper
    {
        void Write(string message);
        void WriteLine(string message);

        ConsoleColor ForegroundColor { get; set; }
        ConsoleColor BackgroundColor { get; set; }
    }
}