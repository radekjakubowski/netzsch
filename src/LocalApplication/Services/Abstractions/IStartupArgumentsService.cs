using System.Collections.Generic;
using System.Printing;
using System.Runtime.InteropServices;

namespace LocalApplication.Services.Abstractions;

public interface IStartupArgumentsService
{
    Dictionary<string, string> ParseStartupArguments(string[] args);
}
