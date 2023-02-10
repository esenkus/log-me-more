using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Selection;
using Avalonia.Interactivity;
using Avalonia.Threading;
using CliWrap.EventStream;
using log_me_more.Models;
using log_me_more.Services;
using log_me_more.ViewModels;
using ReactiveUI;

// ReSharper disable UnusedParameter.Local

namespace log_me_more.Views;

public partial class MainWindow : Window {
    private readonly AdbWrapper adbWrapper = new();
    private readonly LogAnalyzer logAnalyzer = new();
    private readonly HashSet<LogLevel> logLevels;
    private readonly SelectionModel<LogLevel> logLevelSelectionModel;
    private CancellationTokenSource adbLogCommandTokenSource;
    private List<string> devices;
    private ISet<string> logKeys;
    private SelectionModel<string> logKeySelectionModel;

    public MainWindow() {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
        Console.Out.WriteLine("HOWDY");

        logLevelSelectionModel = new SelectionModel<LogLevel> { SingleSelect = false };
        logLevels = LogLevels.getAllLevels().ToHashSet();

        logKeys = new HashSet<string>();

        logKeySelectionModel = new SelectionModel<string> { SingleSelect = false };

        KeyFilterTextBox.ObservableForProperty(textBox => textBox.Text, skipInitial: true)
            .Subscribe(keyFilterTextBoxChanged);
        ValueFilterTextBox.ObservableForProperty(textBox => textBox.Text, skipInitial: true)
            .Subscribe(valueFilterTextBoxChanged);

        devices = adbWrapper.getDevices();
        Console.Out.WriteLine("Connected devices: ");
        devices.ForEach(Console.Out.WriteLine);

        DevicePickerComboBox.Items = devices;

        LogLevelListBox.Items = logLevels;
        LogLevelListBox.Selection = logLevelSelectionModel;
        logLevelSelectionModel.SelectAll();

        LogLevelPanel.Background = LogLevelListBox.Background;
        LogKeyPanel.Background = LogKeyListBox.Background;

        ProcessPickerComboBox.Items = new List<string> { "asd", "qwerty" };
        // tickTock();
    }


    private async void tickTock() {
        var changing = false;
        var timer = new PeriodicTimer(TimeSpan.FromSeconds(5));
        while (await timer.WaitForNextTickAsync()) {
            Console.Out.WriteLine("TICK TICK MOTHERFUCKER");
            if (changing) {
                Console.Out.WriteLine("Adding new entries");
                var newKey = logAnalyzer.addNewLine(
                    "11-14 15:17:24.671  2648  6292  W  android.os.Debug         failed to get memory consumption info: -1");
                recreateLogKeysWithModelAndHandleFiltering(newKey);
            }

            changing = !changing;
        }
    }

    private void handleAdbLoggingForDevice(string deviceId) {
        logAnalyzer.clearLogs();
        logLevelSelectionModel.SelectAll();

        adbLogCommandTokenSource = new CancellationTokenSource();
        var token = adbLogCommandTokenSource.Token;
        adbWrapper.startLogging(deviceId, token).Result.ForEachAsync(commandEvent => {
            switch (commandEvent) {
                case ExitedCommandEvent exited:
                    Console.Out.WriteLine($"Process exited: {exited.ExitCode}");
                    break;
                case StandardErrorCommandEvent stdErr:
                    Console.Out.WriteLine($"Something wrong: {stdErr.Text}");
                    break;
                case StandardOutputCommandEvent stdOut:
                    Console.Out.WriteLine($"output: {stdOut.Text}");
                    var newKey = logAnalyzer.addNewLine(stdOut.Text);
                    Dispatcher.UIThread.InvokeAsync(() => recreateLogKeysWithModelAndHandleFiltering(newKey));
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
        DevicePickerTextBlock.IsVisible = comboBox.SelectedIndex == -1;
        ProcessPickerComboBox.IsEnabled = comboBox.SelectedIndex != -1;
        if (comboBox.SelectedIndex != -1) {
            Console.Out.WriteLine("Selection changed to " + comboBox.SelectedItem);
            handleAdbLoggingForDevice(comboBox.SelectedItem!.ToString()!);
        }
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
        }
        else {
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
        }
        else {
            LogTextBox.Text = logAnalyzer.filterBy(logLevelItems, logKeyItems, valueFilter, keyFilter);
        }

        // LogTextBox.CaretIndex = LogTextBox.Text.LastIndexOf("\n") + 2;
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
        if (adbLogCommandTokenSource != null) {
            adbLogCommandTokenSource.Cancel();
            DevicePickerComboBox.SelectedIndex = -1;
        }

        var logs = Application.Current!.Clipboard!.GetTextAsync().Result;
        Console.Out.WriteLine("Adding logs from clipboard");
        logAnalyzer.loadLog(logs);
        logLevelSelectionModel.SelectAll();
        recreateLogKeysAndHandleFiltering();
        logKeySelectionModel.SelectAll();
    }

    private void openFromFile(object? sender, RoutedEventArgs e) {
        if (adbLogCommandTokenSource != null) {
            adbLogCommandTokenSource.Cancel();
            DevicePickerComboBox.SelectedIndex = -1;
        }

        open();
    }

    private async void open() {
        var dialog = new OpenFileDialog();
        dialog.Filters.Add(new FileDialogFilter { Name = "Text", Extensions = { "txt" } });
        var result = await dialog.ShowAsync(this);
        var pathToFile = result.First();
        var logs = await File.ReadAllTextAsync(pathToFile);
        logAnalyzer.loadLog(logs);
        logLevelSelectionModel.SelectAll();
        recreateLogKeysAndHandleFiltering();
        logKeySelectionModel.SelectAll();
    }

    private void recreateLogKeysAndHandleFiltering() {
        logKeys = logAnalyzer.getAllKeys();
        LogKeyListBox.Items = logKeys;

        LogKeyListBox.Selection = logKeySelectionModel;
        handleLogFiltering();
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void recreateLogKeysWithModelAndHandleFiltering(string? newKey) {
        if (newKey == null) {
            handleLogFiltering();
            return;
        }

        var currentSelections = logKeySelectionModel.SelectedItems.ToHashSet();

        logKeys = logAnalyzer.getAllKeys();
        LogKeyListBox.Items = logKeys.ToArray();
        logKeySelectionModel = new SelectionModel<string> { SingleSelect = false };
        LogKeyListBox.Selection = logKeySelectionModel;
        var source = logKeySelectionModel.Source;
        for (var i = 0; i < source.Count(); i++) {
            var key = source.ElementAt(i);
            if (newKey == key) {
                logKeySelectionModel.Select(i);
            }
            else if (currentSelections.Contains(key)) {
                logKeySelectionModel.Select(i);
            }
        }

        handleLogFiltering();
    }

    private void pickerControlPanelTapped(object? sender, RoutedEventArgs e) {
        Console.Out.WriteLine("Tap!");
        LogLevelPanel.IsVisible = false;
        LogKeyPanel.IsVisible = false;
        PickerControlPanel.IsVisible = false;
    }
}