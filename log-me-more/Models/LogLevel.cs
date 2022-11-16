using System;
using System.Collections.Generic;

namespace log_me_more.Models;

public enum LogLevel {
    VERBOSE,
    DEBUG,
    INFO,
    WARNING,
    ERROR,
    ASSERT
}

public static class Extensions {
    public static string asString(this LogLevel logLevel) {
        var allLower = logLevel.ToString().ToLower();
        return char.ToUpper(allLower[0]) + allLower[1..];
    }

    public static string asShortString(this LogLevel logLevel) {
        return logLevel.ToString()[0].ToString();
    }
}

public static class LogLevels {
    public static List<LogLevel> getAllLevels() {
        return new List<LogLevel> {
            LogLevel.VERBOSE,
            LogLevel.DEBUG,
            LogLevel.INFO,
            LogLevel.WARNING,
            LogLevel.ERROR,
            LogLevel.ASSERT
        };
    }

    public static LogLevel fromLog(string logLevel) {
        return logLevel switch {
            "V" => LogLevel.VERBOSE,
            "D" => LogLevel.DEBUG,
            "I" => LogLevel.INFO,
            "W" => LogLevel.WARNING,
            "E" => LogLevel.ERROR,
            "A" => LogLevel.ASSERT,
            _ => throw new ArgumentException($"Failed to parse {logLevel}")
        };
    }
}