using System;
using log_me_more.Services;

namespace log_me_more.Models;

public class LogLine {
    public DateOnly date { get; private init; }
    public TimeOnly time { get; private init; }
    public int? mainProcessId { get; private init; }
    public int? workerProcessId { get; private init; }
    public LogLevel logLevel { get; private init; }
    public string logKey { get; private init; }
    public string logMessage { get; private init; }


    public static LogLine fromString(string log) {
        if (log.StartsWith("---") || log.StartsWith("  ")) {
            return new LogLine {
                date = new DateOnly(),
                time = new TimeOnly(),
                mainProcessId = 0000,
                workerProcessId = 0000,
                logLevel = LogLevel.INFO,
                logKey = "AndroidRuntime",
                logMessage = log
            };
        }

        var scanner = new Scanner(log);
        var date = scanner.readNext<string>();
        var time = scanner.readNext<string>();
        var possibleBothProcessesIds = scanner.readNext<string>();
        int? mainProcessId = null;
        int? workerProcessId = null;
        string logLevel;
        string logKey;
        if (possibleBothProcessesIds.Contains('-')) {
            mainProcessId = int.Parse(possibleBothProcessesIds.Split("-")[0]);
            var workerProcessIdString = possibleBothProcessesIds.Split("-")[1];
            workerProcessId = int.Parse(workerProcessIdString.Substring(0, workerProcessIdString.IndexOf("/")));
            var levelKey = scanner.readNext<string>().Split("/");
            logLevel = levelKey[0];
            logKey = levelKey[1];
        }
        else if (possibleBothProcessesIds.EndsWith(":")) {
            // no processId, straight to logLevel and key
            logLevel = possibleBothProcessesIds[..^1];
            logKey = scanner.readNext<string>();
        }
        else {
            mainProcessId = int.Parse(possibleBothProcessesIds);
            workerProcessId = scanner.readNext<int>();
            logLevel = scanner.readNext<string>();
            logKey = scanner.readNext<string>();
        }

        var logMessage = scanner.ReadToEnd().Trim();
        return new LogLine {
            date = DateOnly.Parse(date),
            time = TimeOnly.ParseExact(time, "HH:mm:ss.fff"),
            mainProcessId = mainProcessId,
            workerProcessId = workerProcessId,
            logLevel = LogLevels.fromLog(logLevel),
            logKey = logKey,
            logMessage = logMessage
        };
    }

    // TODO: feature - move to LogAnalyzer and allow selection on what parts should be seen (like hide date, process etc.)
    public string toPrintable() {
        var dateString = date.ToString("MM-dd");
        var timeString = time.ToString("HH:mm:ss.fff");
        var logLevelString = logLevel.asShortString();
        return
            $"{dateString,-6}{timeString,-14}{mainProcessId,-6}{workerProcessId,-6}{logLevelString,-3}{logKey,-25}{logMessage}";
    }
}