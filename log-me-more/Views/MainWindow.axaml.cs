using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using log_me_more.Models;
using log_me_more.Services;
using log_me_more.ViewModels;
using ReactiveUI;

namespace log_me_more.Views;

public partial class MainWindow : Window {
    private readonly AdbWrapper adbWrapper = new();
    private readonly List<CheckBoxComboBoxItem> logLevels;
    private List<string> devices;

    public MainWindow() {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
        Console.Out.WriteLine("HOWDY");

        logLevels = LogLevels.getAllLevels()
            .Select(logLevel => new CheckBoxComboBoxItem { name = logLevel, isSelected = false }).ToList();

        KeySearchTextBox.ObservableForProperty(textBox => textBox.Text, skipInitial: true)
            .Subscribe(keySearchTextBoxChanged);
        ValueSearchTextBox.ObservableForProperty(textBox => textBox.Text, skipInitial: true)
            .Subscribe(valueSearchTextBoxChanged);

        devices = adbWrapper.getDevices();
        devices.ForEach(Console.Out.WriteLine);

        devices = FakeDataService.FAKE_DEVICES;

        DevicePickerComboBox.Items = devices;
        LogLevelListBox.Items = logLevels;
        ProcessPickerComboBox.Items = new List<string> { "asd", "qwerty" };
        // LogTextBox.Text = FakeDataService.FAKE_LOG;
    }

    private void devicePickerSelectionChanged(object? sender, SelectionChangedEventArgs e) {
        var comboBox = (ComboBox)sender!;
        Console.Out.WriteLine("Selection changed to " + comboBox.SelectedItem);
        DevicePickerTextBlock.IsVisible = comboBox.SelectedIndex == -1;
        ProcessPickerComboBox.IsEnabled = comboBox.SelectedIndex != -1;
    }

    private void devicePickerTapped(object? sender, RoutedEventArgs e) {
        var newDevices = FakeDataService.FAKE_DEVICES; // adbWrapper.getDevices();
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

    private void processPickerSelectionChanged(object? sender, SelectionChangedEventArgs e) { }

    private void keySearchTextBoxChanged(IObservedChange<TextBox, string> newValue) {
        KeySearchTextBlock.IsVisible = newValue.Value.Length == 0;
        Console.Out.WriteLine("Current text: " + newValue.Value);
        Console.Out.WriteLine("prop text: " + getContext().keySearchText);
    }

    private void valueSearchTextBoxChanged(IObservedChange<TextBox, string> newValue) {
        ValueSearchTextBlock.IsVisible = newValue.Value.Length == 0;
        Console.Out.WriteLine("Current text: " + newValue.Value);
        Console.Out.WriteLine("prop text: " + getContext().valueSearchText);
    }

    private MainWindowViewModel getContext() {
        return (MainWindowViewModel)DataContext!;
    }

    private void logLevelComboBoxSelectionChanged(object? sender, SelectionChangedEventArgs e) { }

    private void logLevelComboBoxTapped(object? sender, RoutedEventArgs e) { }
}