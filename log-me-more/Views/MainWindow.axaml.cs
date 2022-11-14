using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using log_me_more.Services;
using log_me_more.ViewModels;

namespace log_me_more.Views;

public partial class MainWindow : Window {
    private readonly AdbWrapper adbWrapper = new();
    private List<string> devices;

    public MainWindow() {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
        Console.Out.WriteLine("HOWDY");

        devices = adbWrapper.getDevices();
        devices.ForEach(Console.Out.WriteLine);
        DevicePickerComboBox.Items = devices;
        ProcessPickerComboBox.Items = new List<string> { "asd", "qwerty" };
        LogTextBox.Text = FakeDataService.FAKE_LOG;
    }

    private void devicePickerSelectionChanged(object? sender, SelectionChangedEventArgs e) {
        var comboBox = (ComboBox)sender!;
        Console.Out.WriteLine("Selection changed to " + comboBox.SelectedItem);
        if (comboBox.SelectedIndex != -1) DevicePickerTextBlock.Text = "";
    }

    private void devicePickerTapped(object? sender, RoutedEventArgs e) {
        var newDevices = adbWrapper.getDevices();
        if (!devices.SequenceEqual(newDevices)) {
            if (DevicePickerComboBox.SelectedIndex == -1) {
                devices = newDevices;
                DevicePickerComboBox.Items = devices;
                return;
            }
            var currentDevice = devices[DevicePickerComboBox.SelectedIndex];
            devices = newDevices;
            DevicePickerComboBox.Items = devices;
            var currentDeviceIndex = devices.IndexOf(currentDevice);
            if (currentDeviceIndex != -1) DevicePickerComboBox.SelectedIndex = currentDeviceIndex;
            else DevicePickerComboBox.SelectedIndex = -1;
        }
    }

    private void processPickerTapped(object? sender, RoutedEventArgs e) { }

    private void processPickerSelectionChanged(object? sender, SelectionChangedEventArgs e) { }
}