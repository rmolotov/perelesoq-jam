using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Metro.Services.Logging
{
    public class LoggingService : ILoggingService
    {
        private const string DEFAULT_COLOR        = "#e3e3e3";
        private const string INFRASTRUCTURE_COLOR = "#e38d46";
        private const string SERVICES_COLOR       = "#46e372";
        private const string META_COLOR           = "#e346b4";
        private const string GAMEPLAY_COLOR       = "#4697e3";
        
        public void LogMessage(string message, object sender = null) =>
            Debug.Log(GetString(message, sender ?? this));

        public void LogWarning(string message, object sender = null) =>
            Debug.LogWarning(GetString(message, sender ?? this));

        public void LogError(string message, object sender = null) =>
            Debug.LogError(GetString(message, sender ?? this));

        private static string GetString(string message, object sender) =>
            $"<b><i><color={GetHexColor(sender.GetType())}>{sender.GetType().Name}: </color></i></b> {message}";

        private static string GetHexColor(Type sender) =>
            sender.Namespace switch
            {
                var x when Regex.IsMatch(x, @".*Infrastructure.*") => INFRASTRUCTURE_COLOR,
                var x when Regex.IsMatch(x, @".*Meta.*")           => META_COLOR,
                var x when Regex.IsMatch(x, @".*Services.*")       => SERVICES_COLOR,
                var x when Regex.IsMatch(x, @".*Gameplay.*")       => GAMEPLAY_COLOR,
                _                                                                  => DEFAULT_COLOR
            };
    }
}