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
        switch (logLevel) {
            case "V":
            case "VERBOSE":
                return LogLevel.VERBOSE;
            case "D":
            case "DEBUG":
                return LogLevel.DEBUG;
            case "I":
            case "INFO":
                return LogLevel.INFO;
            case "W":
            case "WARN":
                return LogLevel.WARNING;
            case "E":
            case "ERROR":
            case "F": // this one's weird, but seen in wild
                return LogLevel.ERROR;
            case "A":
            case "ASSERT":
                return LogLevel.ASSERT;
            default:
                throw new ArgumentException($"Failed to parse log level: {logLevel}");
        }
    }
}