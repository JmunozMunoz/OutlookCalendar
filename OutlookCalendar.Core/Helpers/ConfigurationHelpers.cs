using System;
using System.Configuration;

namespace OutlookCalendar.Domain.Core.Helpers
{
    public class ConfigurationHelpers
    {
        public static string GetFromConfig(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public static T GetConvertedValueFromConfig<T>(string key, Func<string, T> converter)
        {
            try
            {
                var value = GetFromConfig(key);
                return converter(value);
            }
            catch
            {
                throw new InvalidCastException($"Invalid value of {key} in web.config file.");
            }
        }
    }
}
