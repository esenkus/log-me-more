using System.Collections.Generic;
using System.Linq;
using log_me_more.Models;

namespace log_me_more.Services;

public class LogAnalyzer {
    private List<LogLine> logLines = new();

    public void loadLog(string log) {
        logLines = log.Split("\n").Select(LogLine.fromString).ToList();
    }

    public void addNewLine(string logLine) {
        logLines.Add(LogLine.fromString(logLine));
    }

    public string showAllLogs() {
        return string.Join("\n", logLines.Select(logLine => logLine.ToString()));
    }

    public HashSet<string> getAllKeys() {
        return logLines.Select(logLine => logLine.logKey).OrderBy(logLine => logLine).ToHashSet();
    }

    public string filterBy(HashSet<LogLevel> logLevels, HashSet<string> logKeys, string valueFilter = "",
        string keyFilter = "") {
        var keyFilters = keyFilter.Split(" ").Select(key => key.ToLower()).Where(key => key.Trim().Any()).ToHashSet();

        var filteredLines = logLines
            .Where(logLine => logLevels.Contains(logLine.logLevel))
            .Where(logLine => logKeys.Contains(logLine.logKey));

        if (keyFilter.Any()) {
            filteredLines = filteredLines
                .Where(logLine => keyFilters.Any(key => logLine.logKey.ToLower().Contains(key)));
        }
        if (valueFilter.Any()) {
            filteredLines = filteredLines
                .Where(logLine => logLine.logMessage.ToLower().Contains(valueFilter.ToLower()));
        }
        return string.Join("\n", filteredLines.Select(logLine => logLine.ToString()).ToList());
    }
}