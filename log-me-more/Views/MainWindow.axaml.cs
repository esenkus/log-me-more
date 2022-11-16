using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Selection;
using Avalonia.Interactivity;
using CliWrap.EventStream;
using log_me_more.Models;
using log_me_more.Services;
using log_me_more.ViewModels;
using ReactiveUI;

namespace log_me_more.Views;

public partial class MainWindow : Window {
    private readonly AdbWrapper adbWrapper = new();
    private readonly LogAnalyzer logAnalyzer = new();
    private readonly HashSet<LogLevel> logLevels;
    private readonly SelectionModel<LogLevel> logLevelSelectionModel;
    private List<string> devices;
    private HashSet<string> logKeys;
    private SelectionModel<string> logKeySelectionModel;

    public MainWindow() {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
        Console.Out.WriteLine("HOWDY");

        logLevelSelectionModel = new SelectionModel<LogLevel> { SingleSelect = false };
        logLevels = LogLevels.getAllLevels().ToHashSet();

        // logAnalyzer.loadLog(FakeDataService.FAKE_LOG);
        logKeys = new HashSet<string>(); // = logAnalyzer.getAllKeys();

        logKeySelectionModel = new SelectionModel<string> { SingleSelect = false };

        KeyFilterTextBox.ObservableForProperty(textBox => textBox.Text, skipInitial: true)
            .Subscribe(keyFilterTextBoxChanged);
        ValueFilterTextBox.ObservableForProperty(textBox => textBox.Text, skipInitial: true)
            .Subscribe(valueFilterTextBoxChanged);

        devices = adbWrapper.getDevices();
        Console.Out.WriteLine("Connected devices: ");
        devices.ForEach(Console.Out.WriteLine);

        // devices = FakeDataService.FAKE_DEVICES;

        DevicePickerComboBox.Items = devices;

        LogLevelListBox.Items = logLevels;
        LogLevelListBox.Selection = logLevelSelectionModel;
        logLevelSelectionModel.SelectAll();

        // LogKeyListBox.Items = logKeys;
        // LogKeyListBox.Selection = logKeySelectionModel;
        // logKeySelectionModel.SelectAll();

        LogLevelPanel.Background = LogLevelListBox.Background;
        LogKeyPanel.Background = LogKeyListBox.Background;

        ProcessPickerComboBox.Items = new List<string> { "asd", "qwerty" };
        // LogTextBox.Text = logAnalyzer.showAllLogs();
        // tickTock();
        Console.Out.WriteLine("Wee");
    }


    private async void tickTock() {
        var timer = new PeriodicTimer(TimeSpan.FromSeconds(5));
        while (await timer.WaitForNextTickAsync()) {
            Console.Out.WriteLine("TICK TICK MOTHERFUCKER");
            recreateLogKeysAndHandleFiltering();
        }
    }

    private void handleAdbLoggingForDevice(string deviceId) {
        logAnalyzer.clearLogs();

        adbWrapper.startLogging(deviceId).Result.ForEachAsync(commandEvent => {
            switch (commandEvent) {
                case ExitedCommandEvent exited:
                    Console.Out.WriteLine($"Process exited: {exited.ExitCode}");
                    break;
                case StandardErrorCommandEvent stdErr:
                    Console.Out.WriteLine($"Something wrong: {stdErr.Text}");
                    break;
                case StandardOutputCommandEvent stdOut:
                    Console.Out.WriteLine($"output: {stdOut.Text}");
                    logAnalyzer.addNewLine(stdOut.Text);
                    // logKeys.Clear();
                    // foreach (var key in logAnalyzer.getAllKeys()) {
                    //     logKeys.Add(key);
                    // }
                    // LogKeyListBox.Items = logKeys;
                    // Dispatcher.UIThread.InvokeAsync(recreateLogKeysAndHandleFiltering);
                    break;
                case StartedCommandEvent started:
                    Console.Out.WriteLine($"Process started: {started.ProcessId}");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(commandEvent));
            }
        });
    }

    private void devicePickerSelectionChanged(object? sender, SelectionChangedEventArgs e) {
        var comboBox = (ComboBox)sender!;
        Console.Out.WriteLine("Selection changed to " + comboBox.SelectedItem);
        DevicePickerTextBlock.IsVisible = comboBox.SelectedIndex == -1;
        ProcessPickerComboBox.IsEnabled = comboBox.SelectedIndex != -1;
        handleAdbLoggingForDevice(comboBox.SelectedItem!.ToString()!);
    }

    private void devicePickerTapped(object? sender, RoutedEventArgs e) {
        var newDevices = adbWrapper.getDevices();
        if (devices.SequenceEqual(newDevices)) {
            return;
        }
        if (DevicePickerComboBox.SelectedIndex == -1) {
            devices = newDevices;
            DevicePickerComboBox.Items = devices;
            return;
        }
        var currentDevice = devices[DevicePickerComboBox.SelectedIndex];
        devices = newDevices;
        DevicePickerComboBox.Items = devices;
        var currentDeviceIndex = devices.IndexOf(currentDevice);
        if (currentDeviceIndex != -1) {
            DevicePickerComboBox.SelectedIndex = currentDeviceIndex;
        } else {
            DevicePickerComboBox.SelectedIndex = -1;
        }
    }

    private void processPickerTapped(object? sender, RoutedEventArgs e) { }

    private void processPickerSelectionChanged(object? sender, SelectionChangedEventArgs e) {
        var comboBox = (ComboBox)sender!;
        Console.Out.WriteLine("Selection changed to " + comboBox.SelectedItem);
        ProcessPickerTextBlock.IsVisible = comboBox.SelectedIndex == -1;
    }

    private void keyFilterTextBoxChanged(IObservedChange<TextBox, string> newValue) {
        KeyFilterTextBlock.IsVisible = newValue.Value.Length == 0;
        Console.Out.WriteLine("Current text: " + newValue.Value);
        Console.Out.WriteLine("prop text: " + getContext().keyFilterText);
        handleLogFiltering();
    }

    private void valueFilterTextBoxChanged(IObservedChange<TextBox, string> newValue) {
        ValueFilterTextBlock.IsVisible = newValue.Value.Length == 0;
        Console.Out.WriteLine("Current text: " + newValue.Value);
        Console.Out.WriteLine("prop text: " + getContext().valueFilterText);
        handleLogFiltering();
    }

    private MainWindowViewModel getContext() {
        return (MainWindowViewModel)DataContext!;
    }

    private void logLevelButtonClicked(object? sender, RoutedEventArgs e) {
        LogLevelPanel.IsVisible = true;
        PickerControlPanel.IsVisible = true;
    }

    private void handleLogFiltering() {
        var logLevelItems = logLevelSelectionModel.SelectedItems.ToHashSet();
        var logKeyItems = logKeySelectionModel.SelectedItems.ToHashSet();
        var valueFilter = getContext().valueFilterText;
        var keyFilter = getContext().keyFilterText;

        if (logLevelItems.Count == logLevels.Count && logKeyItems.Count == logKeys.Count
                                                   && !valueFilter.Any() && !keyFilter.Any()) {
            LogTextBox.Text = logAnalyzer.showAllLogs();
            return;
        }
        LogTextBox.Text = logAnalyzer.filterBy(logLevelItems, logKeyItems, valueFilter, keyFilter);
        LogTextBox.CaretIndex = int.MaxValue;
    }

    private void logLevelSelectionChanged(object? sender, SelectionChangedEventArgs e) {
        Console.Out.WriteLine("Selected items changed, we now have: " +
                              string.Join(", ", logLevelSelectionModel.SelectedItems));
        handleLogFiltering();
    }

    private void logLevelSelectAllButtonClicked(object? sender, RoutedEventArgs e) {
        logLevelSelectionModel.SelectAll();
    }

    private void logLevelSelectNoneButtonClicked(object? sender, RoutedEventArgs e) {
        logLevelSelectionModel.DeselectRange(0, int.MaxValue);
    }

    private void logKeyButtonClicked(object? sender, RoutedEventArgs e) {
        LogKeyPanel.IsVisible = true;
        PickerControlPanel.IsVisible = true;
    }

    private void logKeySelectionChanged(object? sender, SelectionChangedEventArgs e) {
        Console.Out.WriteLine("Selected items changed, we now have: " +
                              string.Join(", ", logKeySelectionModel.SelectedItems));
        handleLogFiltering();
    }

    private void logKeySelectAllButtonClicked(object? sender, RoutedEventArgs e) {
        logKeySelectionModel.SelectAll();
    }

    private void logKeySelectNoneButtonClicked(object? sender, RoutedEventArgs e) {
        logKeySelectionModel.DeselectRange(0, int.MaxValue);
    }

    private void pasteFromClipboardButtonClicked(object? sender, RoutedEventArgs e) {
        var logs = Application.Current!.Clipboard!.GetTextAsync().Result;
        logAnalyzer.loadLog(logs);
        logLevelSelectionModel.SelectAll();
        recreateLogKeysAndHandleFiltering();
        logKeySelectionModel.SelectAll();
    }

    private void recreateLogKeysAndHandleFiltering() {
        // TODO: create a new method to simulate timer and new log keys addition, try filtering only some keys, click button and check results, results should contain the new key (select it), but keep all other selections intact
        logKeys = logAnalyzer.getAllKeys();
        LogKeyListBox.Items = logKeys;
        // logKeys.Clear();
        // foreach (var key in logAnalyzer.getAllKeys()) {
        //     logKeys.Add(key);
        // }

        // logKeySelectionModel = new SelectionModel<string> { SingleSelect = false };
        LogKeyListBox.Selection = logKeySelectionModel;
        // logKeySelectionModel.SelectAll();
        handleLogFiltering();
    }

    private void pickerControlPanelTapped(object? sender, RoutedEventArgs e) {
        Console.Out.WriteLine("Tap!");
        LogLevelPanel.IsVisible = false;
        LogKeyPanel.IsVisible = false;
        PickerControlPanel.IsVisible = false;
    }
}