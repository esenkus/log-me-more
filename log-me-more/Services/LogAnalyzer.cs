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

    public string filterBy(HashSet<LogLevel> logLevels, HashSet<string> logKeys, string valueFilter = "") {
        List<string> filteredLines;
        if (valueFilter.Any()) {
            filteredLines = logLines
                .Where(logLine => logLevels.Contains(logLine.logLevel))
                .Where(logLine => logKeys.Contains(logLine.logKey))
                .Where(logLine => logLine.logMessage.ToLower().Contains(valueFilter.ToLower()))
                .Select(logLine => logLine.ToString()).ToList();
        } else {
            filteredLines = logLines
                .Where(logLine => logLevels.Contains(logLine.logLevel))
                .Where(logLine => logKeys.Contains(logLine.logKey))
                .Select(logLine => logLine.ToString()).ToList();
        }
        return string.Join("\n", filteredLines);
    }
}