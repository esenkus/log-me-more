using System.Collections.Generic;
using System.Linq;
using CliWrap;
using CliWrap.Buffered;

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

    public void startLogging(string deviceId) {
        var result = Cli
            .Wrap("adb")
            .WithArguments(new List<string> { "-s", deviceId, "logcat" })
            .ExecuteBufferedAsync();
    }
}