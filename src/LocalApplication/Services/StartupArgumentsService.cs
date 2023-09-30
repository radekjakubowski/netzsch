using System.Collections.Generic;

namespace LocalApplication.Services;

public class StartupArgumentsService
{
    public Dictionary<string, string> ParseStartupArguments(string[] arguments)
    {
        Dictionary<string, string> startupArguments = new Dictionary<string, string>();

        foreach (string arg in arguments)
        {
            string[] parts = arg.Split('=');

            if (parts.Length == 2)
            {
                string key = parts[0];
                string value = parts[1];

                startupArguments[key] = value;
            }
        }

        return startupArguments;
    }
}
