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
}

public static class LogLevels {
    public static List<string> getAllLevels() {
        return new List<string> {
            LogLevel.VERBOSE.asString(),
            LogLevel.DEBUG.asString(),
            LogLevel.INFO.asString(),
            LogLevel.WARNING.asString(),
            LogLevel.ERROR.asString(),
            LogLevel.ASSERT.asString()
        };
    }
}