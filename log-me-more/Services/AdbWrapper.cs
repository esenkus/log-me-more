using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CliWrap;
using CliWrap.Buffered;
using CliWrap.EventStream;

namespace log_me_more.Services;

public class AdbWrapper {
    public List<string> getDevices() {
        var result = Cli
            .Wrap("adb")
            .WithArguments("devices")
            .ExecuteBufferedAsync();

        var devices = result.Task.Result.StandardOutput
            .Split("\n")
            .Skip(1)
            .Select(deviceString =>
                deviceString.Split("\t")[0]
            ).Where(deviceString => deviceString.Length > 0)
            .ToList();

        return devices;
    }

    public async Task<IObservable<CommandEvent>> startLogging(string deviceId) {
        Console.Out.WriteLine($"Trying to log stuff of {deviceId}");
        var cmd = Cli
            .Wrap("adb")
            .WithArguments(new List<string>
                { "-s", deviceId, "logcat", "-T", DateTime.Today.ToString("MM-dd HH:mm:ss.fff") });
        return cmd.Observe();
    }
}