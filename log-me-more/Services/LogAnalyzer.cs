using System;
using System.Collections.Generic;
using System.Linq;
using log_me_more.Models;

namespace log_me_more.Services;

public class LogAnalyzer {
    private readonly SortedSet<string> allLogKeys = new();
    private readonly List<LogLine> logLines = new();

    public void loadLog(string log) {
        foreach (var logLine in log.Split("\n")) {
            if (string.IsNullOrWhiteSpace(logLine)) {
                continue;
            }
            try {
                logLines.Add(LogLine.fromString(logLine));
            } catch (FormatException) {
                Console.Out.WriteLine($"Failed to convert line: {logLine}");
            }
        }
        calculateAllKeys();
    }

    public string? addNewLine(string logLine) {
        var line = LogLine.fromString(logLine);
        logLines.Add(line);
        if (!allLogKeys.Contains(line.logKey)) {
            allLogKeys.Add(line.logKey);
            Console.Out.WriteLine($"New log key added {line.logKey}");
            return line.logKey;
        }
        return null;
    }

    public void clearLogs() {
        logLines.Clear();
        allLogKeys.Clear();
    }

    public string showAllLogs() {
        return string.Join("\n", logLines.Select(logLine => logLine.toPrintable()));
    }

    public ISet<string> getAllKeys() {
        return allLogKeys;
    }

    private void calculateAllKeys() {
        logLines.ForEach(logLine => allLogKeys.Add(logLine.logKey));
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
        return string.Join("\n", filteredLines.Select(logLine => logLine.toPrintable()).ToList());
    }
}