using System;
using System.Collections;
using System.Collections.Generic;

namespace core_web.demo.Config
{
    internal static class EnvironmentSetting
    {
        internal static Dictionary<string, string> Setting { get; private set; }

        static EnvironmentSetting()
        {
            Setting = new Dictionary<string, string>(16, StringComparer.OrdinalIgnoreCase);
            Init(EnvironmentVariableTarget.Process);
            Init(EnvironmentVariableTarget.Machine);
            Init(EnvironmentVariableTarget.User);
        }

        private static void Init(EnvironmentVariableTarget target)
        {
            foreach (DictionaryEntry ev in Environment.GetEnvironmentVariables(target))
            {
                var key = ev.Key?.ToString();
                if (string.IsNullOrEmpty(key))
                {
                    continue;
                }
                Setting[key] = ev.Value?.ToString() ?? string.Empty;
            }
        }

        public static string Get(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }
            Setting.TryGetValue(name, out var value);
            return value;
        }
    }
}
